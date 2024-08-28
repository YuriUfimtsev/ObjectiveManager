namespace ObjectiveManager.Domain.Models;

public record ObjectiveCreation(
    string Definition,
    DateTime FinalDate,
    string? Comment);