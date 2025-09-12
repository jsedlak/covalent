using System.Text.Json.Serialization;

namespace Covalent.Agents.Model;

internal sealed class ToolCall
{
    [JsonPropertyName("id")]
    public string Id { get; set; } = string.Empty;

    [JsonPropertyName("function")]
    public ToolFunction Function { get; set; } = new();

    [JsonPropertyName("type")]
    public string Type { get; set; } = string.Empty;
}