namespace ObjectiveManager.Domain.Dto;

public record CreateObjectiveDto(
    string Definition,
    DateTime FinalDate,
    string? Comment);