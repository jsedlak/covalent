using System.Text.Json.Serialization;

namespace Covalent.Agents.Model;

[GenerateSerializer]
[Alias("MessageContent")]
public sealed class MessageContent
{
    [Id(0)]
    [JsonPropertyName("type")]
    public string Type { get; set; } = string.Empty;

    [Id(1)]
    [JsonPropertyName("text")]
    public string Text { get; set; } = string.Empty;
} 