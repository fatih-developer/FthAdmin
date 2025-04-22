// code: fatih.unal date: 2025-04-22T09:01:40
using MediatR;
using FthAdmin.Application.Features.Auth.Commands;
using FthAdmin.Infrastructure.Contexts;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace FthAdmin.Infrastructure.Features.Auth.Commands
{
    public class RevokeRefreshTokenCommandHandler : IRequestHandler<RevokeRefreshTokenCommand, bool>
    {
        private readonly AppIdentityDbContext _dbContext;
        public RevokeRefreshTokenCommandHandler(AppIdentityDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<bool> Handle(RevokeRefreshTokenCommand request, CancellationToken cancellationToken)
        {
            var refreshToken = _dbContext.RefreshTokens.FirstOrDefault(x => x.Token == request.RefreshToken && !x.IsRevoked);
            if (refreshToken == null)
                return false;
            refreshToken.IsRevoked = true;
            await _dbContext.SaveChangesAsync();
            return true;
        }
    }
}
