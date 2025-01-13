using ObjectiveManager.Domain.Entities;

namespace ObjectiveManager.Application.Models;

// todo: запоминать дату создания
// todo: smart && okr
public record Objective(
    string Id,
    string Definition,
    ObjectiveStatus Status,
    DateTimeOffset FinalDate,
    string? Comment);