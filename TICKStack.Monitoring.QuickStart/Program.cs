using log4net;
using System;
using System.IO;
using System.Reflection;
using System.Threading.Tasks;
using System.Xml;
using TICKStack.Monitoring.QuickStart.Jobs;


namespace TICKStack.Monitoring.QuickStart
{
    class Program
    {
        static readonly ILog _logger = LogManager.GetLogger(typeof(Program));

        static async Task Main(string[] args)
        {
            InitLog4net();

            var job = new PriceMonitoringJob()
            {
                InfluxDbUrl = Helpers.Utils.GetParamValue("INFLUXDB_URL"),
                InfluxDatabaseName = Helpers.Utils.GetParamValue("INFLUXDB_DATABASE_NAME"),
                IntervalInSeconds = Convert.ToInt32(Helpers.Utils.GetParamValue("WRITE_INTERVAL_IN_SECONDS"))
            };
            // var job2 = new HealthMonitoringJob() { InfluxDatabaseName = ConfigurationManager.AppSettings["InfluxDB.Database.Name2"] };

            await job.Execute();

            _logger.Info("End Program");
        }

        static void InitLog4net()
        {
            XmlDocument log4netConfig = new XmlDocument();
            log4netConfig.Load(File.OpenRead("log4net.config"));
            var repo = log4net.LogManager.CreateRepository(Assembly.GetEntryAssembly(),
                       typeof(log4net.Repository.Hierarchy.Hierarchy));
            log4net.Config.XmlConfigurator.Configure(repo, log4netConfig["log4net"]);
        }
    }
}
