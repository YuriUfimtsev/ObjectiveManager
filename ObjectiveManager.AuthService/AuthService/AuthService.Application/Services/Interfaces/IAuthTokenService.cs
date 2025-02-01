using AuthService.Domain.Entities;
using ObjectiveManager.Models.AuthService;

namespace AuthService.Application.Services.Interfaces;

public interface IAuthTokenService
{
    public TokenCredentials GetToken(User user);
}