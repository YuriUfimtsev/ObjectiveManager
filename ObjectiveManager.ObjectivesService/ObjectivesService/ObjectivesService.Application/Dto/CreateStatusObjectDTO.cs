namespace ObjectivesService.Application.Dto;

public record CreateStatusObjectDTO(
    Guid ObjectiveId,
    long StatusValueId,
    string? Comment
);