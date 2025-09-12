using System.Text.Json.Serialization;

namespace Covalent.Agents.Model;

internal sealed class ToolFunction
{
    [JsonPropertyName("arguments")]
    public string Arguments { get; set; } = string.Empty;

    [JsonPropertyName("name")]
    public string Name { get; set; } = string.Empty;
} 