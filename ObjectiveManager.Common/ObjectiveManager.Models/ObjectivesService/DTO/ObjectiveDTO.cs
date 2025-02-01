namespace ObjectiveManager.Models.ObjectivesService.DTO;

public record ObjectiveDTO(
    Guid Id,
    string Definition,
    DateTimeOffset FinalDate,
    string Comment,
    StatusObjectDTO StatusObject);