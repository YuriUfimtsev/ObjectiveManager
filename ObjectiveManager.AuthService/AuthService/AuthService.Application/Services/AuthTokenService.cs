using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using AuthService.Application.Services.Interfaces;
using AuthService.Domain.Entities;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using ObjectiveManager.Models.AuthService;
using ObjectiveManager.Utils.Auth;

namespace AuthService.Application.Services;

public class AuthTokenService : IAuthTokenService
{
    private readonly IConfigurationSection _configuration;
    private readonly JwtSecurityTokenHandler _tokenHandler;

    public AuthTokenService(IConfiguration configuration)
    {
        _configuration = configuration.GetSection("AppSettings");
        _tokenHandler = new JwtSecurityTokenHandler();
    }

    public TokenCredentials GetToken(User user)
    {
        var securityKey = AuthenticationKey.GetSecurityKey();
        
        var timeNow = DateTime.UtcNow;
        var expiresIn = timeNow.AddMinutes(int.Parse(_configuration["ExpiresIn"]));
            
        var token = new JwtSecurityToken(
            issuer: _configuration["ApiName"],
            notBefore: timeNow,
            expires: expiresIn,
            claims: new[]
            {
                new Claim("_id", user.Id),
                new Claim(ClaimTypes.Email, user.Email),
            },
            signingCredentials: new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256));

        var tokenCredentials = new TokenCredentials(
            AccessToken: _tokenHandler.WriteToken(token));

        return tokenCredentials;
    }
}