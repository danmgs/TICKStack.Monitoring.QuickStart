using InfluxDB.Collector;
using log4net;
using System;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Threading.Tasks;

namespace TICKStack.Monitoring.QuickStart.Jobs
{
    public class HealthMonitoringJob : AbstractMonitoringJob
    {
        private static readonly ILog _logger = LogManager.GetLogger(typeof(HealthMonitoringJob));

        public override async Task Execute()
        {
            _logger.Info($"Executes job HealthMonitoringJob");

            var process = Process.GetCurrentProcess();

            Metrics.Collector = new CollectorConfiguration()
                .Tag.With("host", Environment.GetEnvironmentVariable("COMPUTERNAME"))
                .Tag.With("os", Environment.GetEnvironmentVariable("OS"))
                .Tag.With("process", Path.GetFileName(process.MainModule.FileName))
                //.Batch.AtInterval(TimeSpan.FromSeconds(IntervalInSeconds))
                .WriteTo.InfluxDB(InfluxDbUrl, InfluxDatabaseName)
                .CreateCollector();

            while (true)
            {
                var healthStatus = CheckWebsite("http://www.google.com");

                if(healthStatus)
                    Metrics.Increment("iterations");

                _logger.Info($"Inserting in database the datapoint: healthStatus={healthStatus}");

                await Task.Delay(IntervalInSeconds * 1000);
            }
        }

        public bool CheckWebsite(string URL)
        {
            try
            {
                WebClient wc = new WebClient();
                string HTMLSource = wc.DownloadString(URL);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
