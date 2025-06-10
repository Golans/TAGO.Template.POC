using Microsoft.Extensions.Configuration;
using TAGO.Template.Abstractions.Interfaces;
using TAGO.Template.MsSql.DataAccess;
using TAGO.Template.MsSql.DataAccess.DbModel;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class ServiceControllerExtensions
    {
        public static IServiceCollection AddMongoDataAccess(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<DbOptionsBuilder>(builder =>
            {
                builder.ConnectionString = configuration.GetSection("Mongo:Db1:ConnectionString")?.Value;
                builder.Database = configuration.GetSection("Mongo:Db1:Database")?.Value;
                builder.Collection = configuration.GetSection("Mongo:Db1:Collection")?.Value;
            });

            services.AddScoped<IAccountDataAccess, AccountDataAccess>();
            return services;
        }
    }
}
