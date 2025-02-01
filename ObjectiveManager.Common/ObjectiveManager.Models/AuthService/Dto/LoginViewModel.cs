using System.ComponentModel.DataAnnotations;

namespace ObjectiveManager.Models.AuthService.Dto;

public class LoginViewModel
{
    [DataType(DataType.EmailAddress)]
    public required string Email { get; init; }

    [DataType(DataType.Password)]
    public required string Password { get; init; }
}