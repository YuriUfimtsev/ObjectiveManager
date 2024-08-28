using System.Net.WebSockets;

namespace ObjectiveManager.Domain.Models;

public record Objective(
    string Id,
    string Definition,
    ObjectiveStatus Status,
    DateTime FinalDate,
    string? Comment);