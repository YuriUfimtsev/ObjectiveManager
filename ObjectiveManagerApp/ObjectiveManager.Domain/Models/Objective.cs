using System.Net.WebSockets;

namespace ObjectiveManager.Domain.Models;

// Хорошо бы помнить дату создания
// Smart && Okr ?
public record Objective(
    string Id,
    string Definition,
    ObjectiveStatus Status,
    DateTime FinalDate,
    string? Comment);