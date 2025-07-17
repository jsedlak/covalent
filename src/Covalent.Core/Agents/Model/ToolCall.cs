using System.Text.Json.Serialization;

namespace Covalent.Agents.Model;

[GenerateSerializer]
[Alias("ToolCall")]
public sealed class ToolCall
{
    [Id(0)]
    [JsonPropertyName("id")]
    public string Id { get; set; } = string.Empty;

    [Id(1)]
    [JsonPropertyName("function")]
    public ToolFunction Function { get; set; } = new();

    [Id(2)]
    [JsonPropertyName("type")]
    public string Type { get; set; } = string.Empty;
}