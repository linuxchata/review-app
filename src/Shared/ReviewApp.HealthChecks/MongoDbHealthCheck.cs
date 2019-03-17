using System.Threading;
using System.Threading.Tasks;

using Microsoft.Extensions.Diagnostics.HealthChecks;

using MongoDB.Driver;

namespace ReviewApp.HealthChecks
{
    public sealed class MongoDbHealthCheck : IHealthCheck
    {
        private readonly string connectionString;

        public MongoDbHealthCheck(string connectionString)
        {
            this.connectionString = connectionString;
        }

        public async Task<HealthCheckResult> CheckHealthAsync(
            HealthCheckContext context,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            try
            {
                await new MongoClient(this.connectionString).ListDatabasesAsync(cancellationToken);

                return await Task.FromResult(HealthCheckResult.Healthy());
            }
            catch
            {
                return await Task.FromResult(HealthCheckResult.Unhealthy());
            }
        }
    }
}
