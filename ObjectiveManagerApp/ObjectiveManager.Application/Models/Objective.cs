using ObjectiveManager.Domain.Enums;

namespace ObjectiveManager.Application.Models;

// todo: запоминать дату создания
// todo: smart && okr
public record Objective(
    string Id,
    string Definition,
    ObjectiveStatus Status,
    DateTime FinalDate,
    string? Comment);