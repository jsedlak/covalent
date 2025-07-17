using System.Text.Json.Serialization;

namespace Covalent.Agents.Model;

[GenerateSerializer]
[Alias("ToolSchema")]
public sealed class ToolSchema
{
    [Id(0)]
    [JsonPropertyName("name")]
    public string Name { get; set; } = string.Empty;

    [Id(1)]
    [JsonPropertyName("description")]
    public string Description { get; set; } = string.Empty;

    [Id(2)]
    [JsonPropertyName("parameters")]
    public Dictionary<string, object> Parameters { get; set; } = new();

    [Id(3)]
    [JsonPropertyName("type")]
    public string? Type { get; set; }

    [Id(4)]
    [JsonPropertyName("required")]
    public List<string> Required { get; set; } = new();
} 