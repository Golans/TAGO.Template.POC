using Microsoft.Extensions.Configuration;
using Tago.Extensions.Http;
using TAGO.Template.Abstractions.Interfaces;
using TAGO.Template.RestApi.DataAccess;

namespace Microsoft.Extensions.DependencyInjection
{
    internal class RestApiAccountDataAccessSetting
    {
        public int TimeOut { get; set; }

    }


    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddRestApi(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<RestApiAccountDataAccessSetting>(configuration.GetSection("RestApiAccountDataAccess"));

            services.AddRestClient("RestApiAccountDataAccessClient", options =>
            {
            });
            services.AddScoped<IAccountDataAccess, AccountDataAccess>();

            return services;
        }
    }
}
