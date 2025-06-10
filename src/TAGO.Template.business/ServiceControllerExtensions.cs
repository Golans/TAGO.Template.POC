using TAGO.Template.Abstractions.Interfaces;
using TAGO.Template.Business;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class ServiceControllerExtensions
    {
        public static IServiceCollection AddBusinessDi(this IServiceCollection services)
        {

            services.AddScoped<IAccountsManager, AccountsManager>();

            return services;
        }
    }
}
