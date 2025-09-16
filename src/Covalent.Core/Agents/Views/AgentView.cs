namespace Covalent.Agents.Views;

public sealed class AgentView
{
    public string Id { get; set; }

    public string Name { get; set; }

    public string Description { get; set; }

    public string SystemPrompt { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }

    public string Provider { get; set; }

    public string Model { get; set; }
}
