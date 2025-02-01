namespace ObjectivesService.Application.Models.Dto;

public record CreateObjectiveDto(
    string Definition,
    DateTimeOffset FinalDate,
    string? Comment,
    string UserId);