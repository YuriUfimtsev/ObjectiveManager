namespace ObjectivesService.Domain.Entities;

public record ObjectiveEntity : IEntity<Guid>
{
    public Guid Id { get; set; }
    public required DateTimeOffset CreatedAt { get; init; }
    public required string Definition { get; init; }
    public required DateTimeOffset FinalDate {get; init; }
    public string? Comment { get; init; }
    
    public Guid? StatusObjectId { get; set; }
    public StatusObjectEntity? StatusObject { get; set; }
    
    public required string UserId { get; init; }
}