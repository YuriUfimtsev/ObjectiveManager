namespace ObjectivesService.Domain.DTO;

public record UpdateStatusObjectDTO(
    Guid ObjectiveId,
    Guid StatusObjectId);