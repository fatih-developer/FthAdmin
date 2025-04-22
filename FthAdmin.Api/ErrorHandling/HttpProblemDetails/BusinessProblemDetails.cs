// code: fatih.unal date: 2025-04-21T14:12:30
using Microsoft.AspNetCore.Mvc;

namespace FthAdmin.Api.ErrorHandling.HttpProblemDetails
{
    public class BusinessProblemDetails : ProblemDetails
    {
        public BusinessProblemDetails(string detail)
        {
            Title = "Business Rule Violation";
            Status = StatusCodes.Status400BadRequest;
            Detail = detail;
            Type = "https://yourdomain.com/errors/business-rule-violation";
        }
    }
}
