namespace ObjectiveManager.Models.AuthService.Dto;

public record EditAccountDto(
    string Name,
    string Surname,
    string Email,
    string MentorEmail);