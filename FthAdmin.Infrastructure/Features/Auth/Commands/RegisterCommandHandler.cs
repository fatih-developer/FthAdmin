// code: fatih.unal date: 2025-04-22T08:58:44
using MediatR;
using Microsoft.AspNetCore.Identity;
using FthAdmin.Infrastructure.Identity;
using FthAdmin.Application.Features.Auth.Commands;
using System.Threading;
using System.Threading.Tasks;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.Extensions.Options;
using FthAdmin.Infrastructure.Contexts;

namespace FthAdmin.Infrastructure.Features.Auth.Commands
{
    public class RegisterCommandHandler : IRequestHandler<RegisterCommand, AuthResultDto>
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly JwtSettings _jwtSettings;
        private readonly AppIdentityDbContext _identityDbContext;

        public RegisterCommandHandler(UserManager<ApplicationUser> userManager, IOptions<JwtSettings> jwtOptions, AppIdentityDbContext identityDbContext)
        {
            _userManager = userManager;
            _jwtSettings = jwtOptions.Value;
            _identityDbContext = identityDbContext;
        }

        public async Task<AuthResultDto> Handle(RegisterCommand request, CancellationToken cancellationToken)
        {
            var user = new ApplicationUser { UserName = request.UserName, Email = request.Email };
            var result = await _userManager.CreateAsync(user, request.Password);
            if (result.Succeeded)
            {
                var accessToken = GenerateJwtToken(user);
                var refreshToken = GenerateRefreshToken();
                // Refresh token'ı DB'ye kaydet
                var refreshTokenEntity = new RefreshToken
                {
                    Token = refreshToken,
                    UserId = user.Id,
                    ExpiryDate = DateTime.UtcNow.AddDays(7), // Örnek: 7 gün geçerli
                    IsRevoked = false,
                    CreatedAt = DateTime.UtcNow
                };
                _identityDbContext.RefreshTokens.Add(refreshTokenEntity);
                await _identityDbContext.SaveChangesAsync();
                return new AuthResultDto { Success = true, AccessToken = accessToken, RefreshToken = refreshToken };
            }
            return new AuthResultDto { Success = false, ErrorMessage = string.Join(", ", result.Errors) };
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
