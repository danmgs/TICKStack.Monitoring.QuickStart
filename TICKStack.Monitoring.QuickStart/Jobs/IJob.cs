using System.Threading.Tasks;

namespace TICKStack.Monitoring.QuickStart.Jobs
{
    public interface IJob
    {
        string InfluxDatabaseName { get; set; }
        string InfluxDbUrl { get; }
        int IntervalInSeconds { get; }
        Task Execute();
    }
}
