using Covalent.Core.Cqrs;

namespace Covalent.Agents.Events;

[GenerateSerializer]
public sealed class AgentProviderAndModelSetEvent : BaseAgentEvent
{
    [Id(0)]
    public string Provider { get; set; } = null!;

    [Id(1)]
    public string Model { get; set; } = null!;
}