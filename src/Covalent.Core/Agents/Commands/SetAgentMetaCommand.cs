using Covalent.Core.Cqrs;

namespace Covalent.Agents.Commands;

[GenerateSerializer]
public sealed class SetAgentMetaCommand : BaseAgentCommand
{
    [Id(0)]
    public string Name { get; set; } = null!;

    [Id(1)]
    public string Description { get; set; } = null!;
}
