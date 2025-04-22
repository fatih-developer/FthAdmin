// code: fatih.unal date: 2025-04-21T14:12:30
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;
using FthAdmin.Api.ErrorHandling.Handlers;

namespace FthAdmin.Api.ErrorHandling.Middlewares
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionMiddleware> _logger;

        public ExceptionMiddleware(RequestDelegate next, ILogger<ExceptionMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext httpContext)
        {
            try
            {
                await _next(httpContext);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Beklenmeyen bir hata oluştu.");
                // Scoped servisleri burada resolve et
                var exceptionHandler = httpContext.RequestServices.GetService(typeof(HttpExceptionHandler)) as HttpExceptionHandler;
                if (exceptionHandler != null)
                {
                    await exceptionHandler.HandleAsync(httpContext, ex);
                }
                else
                {
                    httpContext.Response.StatusCode = 500;
                    await httpContext.Response.WriteAsync("Beklenmeyen bir hata oluştu.");
                }
            }
        }
    }
}
