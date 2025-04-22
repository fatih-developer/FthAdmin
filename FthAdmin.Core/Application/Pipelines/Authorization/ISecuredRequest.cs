// code: fatih.unal date: 2025-04-21T10:06:55
using MediatR;

namespace FthAdmin.Core.Application.Pipelines.Authorization
{
    /// <summary>
    /// Rol bazlı yetkilendirme gerektiren işlemler için işaret arayüzü.
    /// </summary>
    public interface ISecuredRequest : IRequest
    {
        string[] Roles { get; }
    }
}
