namespace Covalent.Silo.Agents.Model;

[GenerateSerializer]
public class Agent
{
    [Id(0)]
    public string Id { get; set; } = null!;

    [Id(1)]
    public string Name { get; set; } = null!;

    [Id(2)]
    public string Description { get; set; } = null!;

    [Id(3)]
    public string SystemPrompt { get; set; } = null!;

    [Id(4)]
    public string Provider { get; set; } = null!;

    [Id(5)]
    public string Model { get; set; } = null!;
}