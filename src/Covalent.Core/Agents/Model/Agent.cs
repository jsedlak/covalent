using Covalent.Agents.Commands;
using Covalent.Core.Cqrs;

namespace Covalent.Agents.Model;

public class Agent : Aggregate
{
    public void Handle(CreateAgentCommand command) 
    {
        Id = command.AggregateId;
        Name = command.Name;
        Description = command.Description;
    }

    public string Name { get; set; } = null!;

    public string Description { get; set; } = null!;

    public string SystemPrompt { get; set; } = null!;

    public Guid ProviderId { get; set; }

    public Guid[] ToolIds { get; set; } = null!;
}