namespace Covalent.Agents.Model;

[GenerateSerializer]
[Alias("CoreMemory")]
public sealed class CoreMemory
{
    [Id(0)]
    public DateTime CreatedAt { get; set; }

    [Id(1)]
    public string? Description { get; set; }

    [Id(2)]
    public bool IsTemplate { get; set; }

    [Id(3)]
    public string Label { get; set; } = string.Empty;

    [Id(4)]
    public int Limit { get; set; }

    [Id(5)]
    public Dictionary<string, object> Metadata { get; set; } = new();

    [Id(6)]
    public string? TemplateName { get; set; }

    [Id(7)]
    public DateTime UpdatedAt { get; set; }

    [Id(8)]
    public string Value { get; set; } = string.Empty;
} 