namespace ObjectiveManager.Domain.Dto;

public record CreateObjectiveDto(
    string Definition,
    DateTimeOffset FinalDate,
    string? Comment);