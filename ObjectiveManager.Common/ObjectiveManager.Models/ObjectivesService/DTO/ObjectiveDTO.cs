namespace ObjectiveManager.Models.ObjectivesService.DTO;

public record ObjectiveDTO(
    string Id,
    string Definition,
    DateTimeOffset FinalDate,
    string Comment,
    StatusObjectDTO StatusObject);