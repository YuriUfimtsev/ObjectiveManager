namespace ObjectiveManager.Domain.Entities;

public record ObjectiveStatusEntity : IEntity<long>
{
    public long Id { get; set; }
    
    public required string Name { get; init; }
}