namespace Covalent.Agents.Model;

[GenerateSerializer]
[Alias("ToolSchema")]
public sealed class ToolSchema
{
    [Id(0)]
    public string Name { get; set; } = string.Empty;

    [Id(1)]
    public string Description { get; set; } = string.Empty;

    [Id(2)]
    public Dictionary<string, object> Parameters { get; set; } = new();

    [Id(3)]
    public string? Type { get; set; }

    [Id(4)]
    public List<string> Required { get; set; } = new();
} 