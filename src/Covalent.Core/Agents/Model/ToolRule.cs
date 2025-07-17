namespace Covalent.Agents.Model;

[GenerateSerializer]
[Alias("ToolRule")]
public sealed class ToolRule
{
    [Id(0)]
    public string ToolName { get; set; } = string.Empty;

    [Id(1)]
    public string Type { get; set; } = string.Empty;
} 