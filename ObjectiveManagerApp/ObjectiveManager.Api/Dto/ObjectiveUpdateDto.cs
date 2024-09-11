using ObjectiveManager.Domain.Enums;

namespace ObjectiveManager.Api.Dto;

public record ObjectiveUpdateDto(
    string Id,
    string Definition,
    ObjectiveStatus Status,
    DateTime FinalDate,
    string? Comment);