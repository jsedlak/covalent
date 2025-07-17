using System.Text.Json.Serialization;

namespace Covalent.Agents.Model;

[GenerateSerializer]
[Alias("ToolRule")]
public sealed class ToolRule
{
    [Id(0)]
    [JsonPropertyName("tool_name")]
    public string ToolName { get; set; } = string.Empty;

    [Id(1)]
    [JsonPropertyName("type")]
    public string Type { get; set; } = string.Empty;
} 