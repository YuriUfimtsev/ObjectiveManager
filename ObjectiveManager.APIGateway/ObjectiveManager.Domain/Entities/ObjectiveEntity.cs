namespace ObjectiveManager.Domain.Entities;

public record ObjectiveEntity : IEntity<Guid>
{
    public Guid Id { get; set; }
    public required string Definition { get; init; }
    public required DateTimeOffset FinalDate {get; init; }
    public required string Comment { get; init; }
    
    public required long StatusId { get; init; }
    public ObjectiveStatusEntity? Status { get; set; }
}