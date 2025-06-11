using Microsoft.Extensions.Configuration;
using TAGO.Template.Abstractions.Interfaces;
using TAGO.Template.MsSql.DataAccess;
using TAGO.Template.MsSql.DataAccess.DbModel;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddMsSqlDataAccess(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<DbOptionsBuilder>(builder =>
            {
                builder.ConnectionString = configuration.GetSection("Sql:Db1:ConnectionString")?.Value;
            });

            services.AddScoped<IAccountDataAccess, AccountDataAccess>();
            return services;
        }
    }
}
