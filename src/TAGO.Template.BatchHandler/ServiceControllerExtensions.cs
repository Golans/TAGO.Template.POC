using Tago.Extensions.AspNetCore.Batch.Model;
using TAGO.Template.Batch;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class ServiceControllerExtensions
    {
        public static IServiceCollection AddBatchJob(this IServiceCollection services, Action<AsyncJobsSettings> configuration)
        {
            services.AddAsyncJobsExecutorServices(configuration);
            services.AddSingleton<BatchHandler>();

            return services;
        }
    }
}
