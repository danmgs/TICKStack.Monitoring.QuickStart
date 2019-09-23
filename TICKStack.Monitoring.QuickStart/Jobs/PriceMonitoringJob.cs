using InfluxDB.Collector;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Threading.Tasks;

namespace TICKStack.Monitoring.QuickStart.Jobs
{
    public class PriceMonitoringJob : AbstractMonitoringJob
    {
        public override async Task Execute()
        {
            var process = Process.GetCurrentProcess();

            Metrics.Collector = new CollectorConfiguration()
                .Tag.With("host", Environment.GetEnvironmentVariable("COMPUTERNAME"))
                .Tag.With("os", Environment.GetEnvironmentVariable("OS"))
                .Tag.With("process", Path.GetFileName(process.MainModule.FileName))
                .Batch.AtInterval(TimeSpan.FromSeconds(IntervalInSeconds))
                .WriteTo.InfluxDB(InfluxDbUrl, InfluxDatabaseName)
                .CreateCollector();

            while (true)
            {
                Metrics.Increment("iterations");

                Random rnd = new Random();

                var tup = GeneratePrices(0.1, 10.0);

                Metrics.Write("price",
                    new Dictionary<string, object>
                    {
                        { "bid", tup.Item1 },
                        { "ask", tup.Item2 }
                    });

                await Task.Delay(1000);
            }
        }

        static float GenerateRandomFloat(Random rnd)
        {
            return (float)(float.MaxValue * 2.0 * (rnd.NextDouble() - 0.5));
        }

        static double GetRandomNumber(double minimum, double maximum)
        {
            Random random = new Random();
            return random.NextDouble() * (maximum - minimum) + minimum;
        }

        static Tuple<double, double> GeneratePrices(double minimum, double maximum)
        {
            var a = GetRandomNumber(minimum, maximum);
            var b = GetRandomNumber(minimum, maximum);
            double bid, ask;
            if(a > b)
            {
                ask = a;
                bid = b;
            }
            else
            {
                bid = a;
                ask = b;
            }

            return Tuple.Create(bid, ask);
        }
    }
}
