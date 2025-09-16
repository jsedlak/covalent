namespace Covalent.Core.Cqrs;

[GenerateSerializer]
public abstract class AggregateCommand
{
    [Id(0)]
    public string AggregateId { get; set; } = null!;
}
