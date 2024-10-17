namespace ObjectiveManager.Api.Dto;

public record ObjectivePostDto(
    string Definition,
    DateTime FinalDate,
    string? Comment);