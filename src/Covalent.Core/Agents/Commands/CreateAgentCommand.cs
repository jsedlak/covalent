using Covalent.Core.Cqrs;

namespace Covalent.Agents.Commands;

public sealed class CreateAgentCommand : AggregateCommand
{
    public string Name { get; set; } = null!;
    
    public string Description { get; set; } = null!;
}