using Tago.Extensions.AspNetCore;
using TAGO.Template.Abstractions.Exceptions;

namespace TAGO.Template.Api.Extensions
{
    public class BusinessExeptionConverterMiddleware
    {
        private readonly RequestDelegate next;
        private readonly ILogger<BusinessExeptionConverterMiddleware> logger;

        public BusinessExeptionConverterMiddleware(
            RequestDelegate next,
            ILogger<BusinessExeptionConverterMiddleware> logger)
        {
            this.next = next;
            this.logger = logger;
        }
        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                try
                {
                    await next(context);
                }
                catch (AggregateException ex)
                {
                    if (ex.InnerExceptions?.Count == 1)
                    {
                        throw ex.InnerExceptions[0];
                    }
                    throw;
                }
            }
            catch (NotFoundException ex)
            {
                throw new BaseAspNetCoreException(StatusCodes.Status404NotFound, ex.Message, ex);
            }
            catch (UnauthorizedAccessException ex)
            {
                throw new BaseAspNetCoreException(StatusCodes.Status401Unauthorized, ex.Message, ex);
            }
            catch (BaseException ex)
            {
                throw new BaseAspNetCoreException(StatusCodes.Status500InternalServerError, ex.Message, ex);
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}
