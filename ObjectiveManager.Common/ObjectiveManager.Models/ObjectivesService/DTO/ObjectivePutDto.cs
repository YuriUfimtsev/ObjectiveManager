namespace ObjectiveManager.Models.ObjectivesService.DTO;

public record ObjectivePutDto(
    string Definition,
    DateTimeOffset FinalDate,
    string? Comment);