namespace Covalent.Agents.Model;

[GenerateSerializer]
[Alias("Tool")]
public sealed class Tool
{
    [Id(0)]
    public Dictionary<string, object>? ArgsJsonSchema { get; set; }

    [Id(1)]
    public DateTime CreatedAt { get; set; }

    [Id(2)]
    public string Description { get; set; } = string.Empty;

    [Id(3)]
    public ToolSchema JsonSchema { get; set; } = new();

    [Id(4)]
    public string Name { get; set; } = string.Empty;

    [Id(5)]
    public int ReturnCharLimit { get; set; }

    [Id(6)]
    public string? SourceCode { get; set; }

    [Id(7)]
    public string SourceType { get; set; } = string.Empty;

    [Id(8)]
    public List<string> Tags { get; set; } = new();

    [Id(9)]
    public string ToolType { get; set; } = string.Empty;

    [Id(10)]
    public DateTime UpdatedAt { get; set; }

    [Id(11)]
    public Dictionary<string, object> Metadata { get; set; } = new();
} 