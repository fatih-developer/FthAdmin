// code: fatih.unal date: 2025-04-21T10:06:55
using MediatR;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using FthAdmin.Core.CrossCuttingConcerns.Exceptions;

namespace FthAdmin.Core.Application.Pipelines.Authorization
{
    public class AuthorizationBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse> where TRequest : IRequest<TResponse>
    {
        private readonly ICurrentUserService _currentUserService;
        public AuthorizationBehavior(ICurrentUserService currentUserService)
        {
            _currentUserService = currentUserService;
        }
        public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
            if (request is ISecuredRequest securedRequest)
            {
                var userRoles = _currentUserService.Roles;
                if (securedRequest.Roles == null || !securedRequest.Roles.Any())
                    throw new BusinessException("Yetki gerektiren bir işlem için rol tanımı yapılmamış.");
                if (!securedRequest.Roles.Any(role => userRoles.Contains(role)))
                    throw new BusinessException("Bu işlemi gerçekleştirmek için yetkiniz yok.");
            }
            return await next();
        }
    }
    public interface ICurrentUserService
    {
        int UserId { get; }
        string UserName { get; }
        IList<string> Roles { get; }
    }
}
