using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Tago.Extensions.AspNetCore
{
    public class BaseAspNetCoreException : Exception
    {
        public BaseAspNetCoreException(int statusCode, string message, Exception inner = null) : base(message, inner)
        {
            StatusCode = statusCode;
        }

        public int StatusCode { get; }
    }

    public class BaseHttpException
    {
        public string Type { get; set; }
        public string Title { get; set; }
        public int Status { get; set; }
    }

    public static class ServiceCollectionExtensions
    {
        public static WebApplicationBuilder ConfigureBaseApp(this WebApplicationBuilder builder)
        {
            return builder;
        }

        public static IServiceCollection AddBaseAppServices(this IServiceCollection services, IConfiguration configuration)
        {
            return services;
        }

        public static IHost UseBaseApp_BFB(this IHost services)
        {
            return services;
        }

        public static IHost UseBaseApp_BFF(this IHost services)
        {
            return services;
        }
    }

    public class BaseServicesBuilder
    {
        public static BaseServicesBuilder Default = new BaseServicesBuilder();

        public void WithJwtAuthorization(bool v)
        {
            throw new NotImplementedException();
        }
    }
}
