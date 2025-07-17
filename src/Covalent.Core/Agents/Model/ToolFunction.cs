namespace Covalent.Agents.Model;

[GenerateSerializer]
[Alias("ToolFunction")]
public sealed class ToolFunction
{
    [Id(0)]
    public string Arguments { get; set; } = string.Empty;

    [Id(1)]
    public string Name { get; set; } = string.Empty;
} 