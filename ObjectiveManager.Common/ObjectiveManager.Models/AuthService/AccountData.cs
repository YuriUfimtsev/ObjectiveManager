namespace ObjectiveManager.Models.AuthService;

public record AccountData(
    string UserId,
    string Name,
    string Surname,
    string Email,
    string MentorEmail);