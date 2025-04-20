namespace ObjectiveManager.Models.ObjectivesService.DTO;

public record StatusObjectDTO(
    DateTimeOffset CreatedAt,
    StatusValueDTO StatusValue,
    string Comment);