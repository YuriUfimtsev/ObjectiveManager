using AuthService.Domain.Entities;
using Microsoft.AspNetCore.Identity;

namespace AuthService.Application.Services.Interfaces;

public interface IUserManager
{
    public Task<IdentityResult> CreateAsync(User user, string password);
    public Task<User?> FindByIdAsync(string id);
    public Task<User?> FindByEmailAsync(string email);
    public Task<IdentityResult> UpdateAsync(User user);
}