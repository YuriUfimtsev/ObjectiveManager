namespace ObjectivesService.Domain.Entities;

public record StatusValueEntity : IEntity<long>
{
    public long Id { get; set; }
    
    public required string Name { get; init; }
}