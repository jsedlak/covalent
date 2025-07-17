namespace Covalent.Agents.Model;

[GenerateSerializer]
[Alias("ToolCall")]
public sealed class ToolCall
{
    [Id(0)]
    public string Id { get; set; } = string.Empty;

    [Id(1)]
    public ToolFunction Function { get; set; } = new();

    [Id(2)]
    public string Type { get; set; } = string.Empty;
} 