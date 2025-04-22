// code: fatih.unal date: 2025-04-21T14:12:30
using Microsoft.AspNetCore.Mvc;

namespace FthAdmin.Api.ErrorHandling.HttpProblemDetails
{
    public class InternalServerErrorProblemDetails : ProblemDetails
    {
        public InternalServerErrorProblemDetails(string detail)
        {
            Title = "Internal Server Error";
            Status = StatusCodes.Status500InternalServerError;
            Detail = detail;
            Type = "https://yourdomain.com/errors/internal-server-error";
        }
    }
}
