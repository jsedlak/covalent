using Covalent.Core.Cqrs;

namespace Covalent.Agents.Events;

[GenerateSerializer]
public abstract class BaseAgentEvent : AggregateEvent
{
}