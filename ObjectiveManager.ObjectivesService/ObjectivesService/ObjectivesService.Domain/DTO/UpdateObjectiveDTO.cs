namespace ObjectivesService.Domain.DTO;

public record UpdateObjectiveDTO(
    Guid ObjectiveId,
    string Definition,
    DateTimeOffset FinalDate,
    string? Comment);