using Covalent.Core.Cqrs;

namespace Covalent.Agents.Events;

[GenerateSerializer]
public sealed class AgentSystemPromptSetEvent : BaseAgentEvent
{
    [Id(0)]
    public string SystemPrompt { get; set; } = null!;
}
