namespace ObjectiveManager.Models.ObjectivesService.DTO;

public record ObjectivePostDto(
    string Definition,
    DateTimeOffset FinalDate,
    string? Comment);