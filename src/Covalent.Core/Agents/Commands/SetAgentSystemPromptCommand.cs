namespace Covalent.Agents.Commands;

[GenerateSerializer]
public sealed class SetAgentSystemPromptCommand : BaseAgentCommand
{
    [Id(0)]
    public string SystemPrompt { get; set; } = null!;
}
