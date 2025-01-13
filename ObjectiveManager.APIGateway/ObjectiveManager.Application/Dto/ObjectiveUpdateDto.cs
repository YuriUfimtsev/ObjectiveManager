namespace ObjectiveManager.Application.Dto;

public record UpdateObjectiveDto(
    string Id,
    string Definition,
    long StatusId,
    DateTimeOffset FinalDate,
    string? Comment);