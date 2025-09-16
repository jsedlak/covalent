namespace Covalent.Core.Cqrs;

[GenerateSerializer]
public abstract class AggregateEvent
{
    [Id(0)]
    public Guid AggregateId { get; set; }
}