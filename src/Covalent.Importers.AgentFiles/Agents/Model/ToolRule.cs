using System.Text.Json.Serialization;

namespace Covalent.Agents.Model;

internal sealed class ToolRule
{
    [JsonPropertyName("tool_name")]
    public string ToolName { get; set; } = string.Empty;

    [JsonPropertyName("type")]
    public string Type { get; set; } = string.Empty;
} 