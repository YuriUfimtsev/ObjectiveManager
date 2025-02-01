namespace ObjectivesService.Domain.Entities;

public record StatusObjectEntity : IEntity<Guid>
{ 
    public Guid Id { get; set; } 
    
    public required DateTimeOffset CreatedAt { get; init; }

    public required Guid ObjectiveId { get; init; }
    
    public required long StatusValueId { get; init; }
    public StatusValueEntity? StatusValue { get; set; }
    
    public string? Comment { get; init; }
}