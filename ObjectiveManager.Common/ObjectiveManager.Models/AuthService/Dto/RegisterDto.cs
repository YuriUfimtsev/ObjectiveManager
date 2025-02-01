namespace ObjectiveManager.Models.AuthService.Dto;

public record RegisterDto(
    string Name,
    string Surname,
    string Email,
    string Password,
    string MentorEmail);