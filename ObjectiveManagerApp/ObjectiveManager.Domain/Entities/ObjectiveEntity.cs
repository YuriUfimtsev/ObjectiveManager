using ObjectiveManager.Domain.Enums;

namespace ObjectiveManager.Domain.Entities;

public record ObjectiveEntity(
    string Id,
    string Definition,
    ObjectiveStatus Status,
    DateTime FinalDate,
    string? Comment);