// code: fatih.unal date: 2025-04-22T09:00:23
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using FthAdmin.Infrastructure.Identity;
using FthAdmin.Infrastructure.Contexts;
using FthAdmin.Application.Features.Auth.Commands;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace FthAdmin.Infrastructure.Features.Auth.Commands
{
    public class RefreshTokenCommandHandler : IRequestHandler<RefreshTokenCommand, AuthResultDto>
    {
        private readonly AppIdentityDbContext _dbContext;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly JwtSettings _jwtSettings;

        public RefreshTokenCommandHandler(AppIdentityDbContext dbContext, UserManager<ApplicationUser> userManager, IOptions<JwtSettings> jwtOptions)
        {
            _dbContext = dbContext;
            _userManager = userManager;
            _jwtSettings = jwtOptions.Value;
        }

        public async Task<AuthResultDto> Handle(RefreshTokenCommand request, CancellationToken cancellationToken)
        {
            var refreshTokenEntity = _dbContext.RefreshTokens.FirstOrDefault(x => x.Token == request.RefreshToken && !x.IsRevoked && x.ExpiryDate > DateTime.UtcNow);
            if (refreshTokenEntity == null)
                return new AuthResultDto { Success = false, ErrorMessage = "Geçersiz veya süresi dolmuş refresh token." };

            var user = await _userManager.FindByIdAsync(refreshTokenEntity.UserId.ToString());
            if (user == null)
                return new AuthResultDto { Success = false, ErrorMessage = "Kullanıcı bulunamadı." };

            var newAccessToken = GenerateJwtToken(user);
            // Güvenlik için refresh token'ı yenile (rotation)
            var newRefreshToken = GenerateRefreshToken();
            refreshTokenEntity.IsRevoked = true;
            var refreshTokenNewEntity = new RefreshToken
            {
                Token = newRefreshToken,
                UserId = user.Id,
                ExpiryDate = DateTime.UtcNow.AddDays(7),
                IsRevoked = false,
                CreatedAt = DateTime.UtcNow
            };
            _dbContext.RefreshTokens.Add(refreshTokenNewEntity);
            await _dbContext.SaveChangesAsync();

            return new AuthResultDto { Success = true, AccessToken = newAccessToken, RefreshToken = newRefreshToken };
        }

        private string GenerateJwtToken(ApplicationUser user)
        {
            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.UserName ?? ""),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString())
            };
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.Key));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var token = new JwtSecurityToken(
                issuer: _jwtSettings.Issuer,
                audience: _jwtSettings.Audience,
                claims: claims,
                expires: DateTime.Now.AddMinutes(_jwtSettings.AccessTokenExpireMinutes),
                signingCredentials: creds
            );
            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        private string GenerateRefreshToken()
        {
            return Guid.NewGuid().ToString().Replace("-", "");
        }
    }
}
