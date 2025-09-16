namespace Covalent.Agents.Commands;

[GenerateSerializer]
public sealed class SetAgentProviderAndModelCommand : BaseAgentCommand
{
    [Id(0)]
    public string Provider { get; set; } = null!;

    [Id(1)]
    public string Model { get; set; } = null!;
}
