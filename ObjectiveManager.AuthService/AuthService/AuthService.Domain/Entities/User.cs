using Microsoft.AspNetCore.Identity;

namespace AuthService.Domain.Entities;

public class User : IdentityUser
{
    public string Name { get; set; }

    public string Surname { get; set; }
    
    public string MentorEmail { get; set; }
}