using AuthService.Application.Services.Interfaces;
using AuthService.Domain.Entities;
using Microsoft.AspNetCore.Identity;

namespace AuthService.Application.Services;

public class ProxyUserManager(UserManager<User> aspUserManager) : IUserManager
{
    public Task<IdentityResult> CreateAsync(User user, string password)
        => aspUserManager.CreateAsync(user, password);

    public Task<User?> FindByIdAsync(string id)
        => aspUserManager.FindByIdAsync(id);

    public Task<User?> FindByEmailAsync(string email)
        => aspUserManager.FindByEmailAsync(email);

    public Task<IdentityResult> UpdateAsync(User user)
        => aspUserManager.UpdateAsync(user);
}