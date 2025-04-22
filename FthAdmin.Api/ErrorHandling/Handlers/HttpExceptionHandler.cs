// code: fatih.unal date: 2025-04-21T14:12:30
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Net;
using System.Text.Json;
using System.Threading.Tasks;
using FthAdmin.Core.CrossCuttingConcerns.Exceptions;
using FthAdmin.Api.ErrorHandling.HttpProblemDetails;

namespace FthAdmin.Api.ErrorHandling.Handlers
{
    public class HttpExceptionHandler
    {
        public async Task HandleAsync(HttpContext context, Exception exception)
        {
            context.Response.ContentType = "application/problem+json";
            ProblemDetails problemDetails = exception switch
            {
                BusinessException be => new BusinessProblemDetails(be.Message),
                _ => new InternalServerErrorProblemDetails("Beklenmeyen bir hata oluştu.")
            };
            context.Response.StatusCode = problemDetails.Status ?? (int)HttpStatusCode.InternalServerError;
            var json = JsonSerializer.Serialize(problemDetails, new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase });
            await context.Response.WriteAsync(json);
        }
    }
}
