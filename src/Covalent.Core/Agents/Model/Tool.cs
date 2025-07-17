using System.Text.Json.Serialization;

namespace Covalent.Agents.Model;

[GenerateSerializer]
[Alias("Tool")]
public sealed class Tool
{
    [Id(0)]
    [JsonPropertyName("args_json_schema")]
    public Dictionary<string, object>? ArgsJsonSchema { get; set; }

    [Id(1)]
    [JsonPropertyName("created_at")]
    public DateTime CreatedAt { get; set; }

    [Id(2)]
    [JsonPropertyName("description")]
    public string Description { get; set; } = string.Empty;

    [Id(3)]
    [JsonPropertyName("json_schema")]
    public ToolSchema JsonSchema { get; set; } = new();

    [Id(4)]
    [JsonPropertyName("name")]
    public string Name { get; set; } = string.Empty;

    [Id(5)]
    [JsonPropertyName("return_char_limit")]
    public int ReturnCharLimit { get; set; }

    [Id(6)]
    [JsonPropertyName("source_code")]
    public string? SourceCode { get; set; }

    [Id(7)]
    [JsonPropertyName("source_type")]
    public string SourceType { get; set; } = string.Empty;

    [Id(8)]
    [JsonPropertyName("tags")]
    public List<string> Tags { get; set; } = new();

    [Id(9)]
    [JsonPropertyName("tool_type")]
    public string ToolType { get; set; } = string.Empty;

    [Id(10)]
    [JsonPropertyName("updated_at")]
    public DateTime UpdatedAt { get; set; }

    [Id(11)]
    [JsonPropertyName("metadata_")]
    public Dictionary<string, object> Metadata { get; set; } = new();
} 