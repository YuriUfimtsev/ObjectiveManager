namespace ObjectiveManager.Api.Dto;

public record ObjectivePutDto(
    string Id,
    string Definition,
    long StatusId,
    DateTimeOffset FinalDate,
    string? Comment);