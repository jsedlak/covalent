using System.Text.Json.Serialization;

namespace Covalent.Agents.Model;

internal sealed class MessageContent
{
    [JsonPropertyName("type")]
    public string Type { get; set; } = string.Empty;

    [JsonPropertyName("text")]
    public string Text { get; set; } = string.Empty;
} 