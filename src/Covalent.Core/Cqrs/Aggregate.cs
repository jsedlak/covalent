namespace Covalent.Core.Cqrs;

public abstract class Aggregate {
    public Guid Id { get; set; } = Guid.NewGuid();
}