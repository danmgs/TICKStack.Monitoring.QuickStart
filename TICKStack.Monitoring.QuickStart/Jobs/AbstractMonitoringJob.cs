using System.Threading.Tasks;

namespace TICKStack.Monitoring.QuickStart.Jobs
{
    public abstract class AbstractMonitoringJob : IJob
    {
        public string InfluxDatabaseName { get; set; }

        public string InfluxDbUrl { get; set; }

        public int IntervalInSeconds { get; set; }

        public abstract Task Execute();
    }
}
