using System;
using System.Configuration;
using System.Threading.Tasks;

namespace TICKStack.Monitoring.QuickStart.Jobs
{
    public abstract class AbstractMonitoringJob : IJob
    {
        public string InfluxDatabaseName { get; set; }

        public string InfluxDbUrl => ConfigurationManager.AppSettings["InfluxDB.Url"];

        public int IntervalInSeconds => Convert.ToInt32(ConfigurationManager.AppSettings["IntervalInSeconds"]);

        public abstract Task Execute();
    }
}
