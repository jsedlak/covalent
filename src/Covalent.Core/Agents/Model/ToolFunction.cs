using System.Text.Json.Serialization;

namespace Covalent.Agents.Model;

[GenerateSerializer]
[Alias("ToolFunction")]
public sealed class ToolFunction
{
    [Id(0)]
    [JsonPropertyName("arguments")]
    public string Arguments { get; set; } = string.Empty;

    [Id(1)]
    [JsonPropertyName("name")]
    public string Name { get; set; } = string.Empty;
} 