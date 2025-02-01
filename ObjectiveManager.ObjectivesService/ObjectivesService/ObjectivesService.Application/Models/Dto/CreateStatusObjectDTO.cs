namespace ObjectivesService.Application.Models.Dto;

public record CreateStatusObjectDTO(
    Guid ObjectiveId,
    long StatusValueId,
    string? Comment);