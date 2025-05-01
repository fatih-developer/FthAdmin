// code: fatih.unal date: 2025-04-22T17:01:48
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
    /// <summary>
    /// Kullanıcı girişi işlemlerini yöneten handler.
    /// </summary>
    public class LoginCommandHandler : IRequestHandler<LoginCommand, AuthResultDto>
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly JwtSettings _jwtSettings;
        private readonly AppIdentityDbContext _identityDbContext;

        public LoginCommandHandler(
            UserManager<ApplicationUser> userManager,
            IOptions<JwtSettings> jwtOptions,
            AppIdentityDbContext identityDbContext)
        {
            _userManager = userManager;
            _jwtSettings = jwtOptions.Value;
            _identityDbContext = identityDbContext;
        }

        public async Task<AuthResultDto> Handle(LoginCommand request, CancellationToken cancellationToken)
        {
            var user = await _userManager.FindByNameAsync(request.UserName);
            if (user == null)
                return new AuthResultDto { Success = false, ErrorMessage = "Kullanıcı bulunamadı." };

            var passwordValid = await _userManager.CheckPasswordAsync(user, request.Password);
            if (!passwordValid)
                return new AuthResultDto { Success = false, ErrorMessage = "Kullanıcı adı veya şifre hatalı." };

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

        private string GenerateJwtToken(ApplicationUser user)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_jwtSettings.Key);
            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.UserName ?? ""),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString())
            };
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.Now.AddMinutes(_jwtSettings.AccessTokenExpireMinutes),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }

        private string GenerateRefreshToken()
        {
            return Guid.NewGuid().ToString().Replace("-", "");
        }
    }
}
