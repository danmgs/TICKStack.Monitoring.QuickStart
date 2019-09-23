using log4net;
using System;
using System.Configuration;
using System.IO;
using System.Reflection;
using System.Threading.Tasks;
using System.Xml;
using TICKStack.Monitoring.QuickStart.Jobs;

namespace TICKStack.Monitoring.QuickStart
{
    class Program
    {
        static readonly ILog log = LogManager.GetLogger(typeof(Program));

        static async Task Main(string[] args)
        {
            InitLog4net();

            var job = new PriceMonitoringJob() { InfluxDatabaseName = ConfigurationManager.AppSettings["InfluxDB.Database.Name"] };
            await job.Execute();

            Console.WriteLine("Press any key to exit");
            Console.ReadKey();
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
