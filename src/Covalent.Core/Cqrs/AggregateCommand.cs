namespace Covalent.Core.Cqrs;

public abstract class AggregateCommand
{
    public Guid AggregateId { get; set; }
}
