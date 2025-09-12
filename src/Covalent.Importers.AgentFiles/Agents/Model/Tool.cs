using System.Text.Json.Serialization;

namespace Covalent.Agents.Model;

internal sealed class Tool
{
    [JsonPropertyName("args_json_schema")]
    public Dictionary<string, object>? ArgsJsonSchema { get; set; }

    [JsonPropertyName("created_at")]
    public DateTime CreatedAt { get; set; }

    [JsonPropertyName("description")]
    public string Description { get; set; } = string.Empty;

    [JsonPropertyName("json_schema")]
    public ToolSchema JsonSchema { get; set; } = new();

    [JsonPropertyName("name")]
    public string Name { get; set; } = string.Empty;

    [JsonPropertyName("return_char_limit")]
    public int ReturnCharLimit { get; set; }

    [JsonPropertyName("source_code")]
    public string? SourceCode { get; set; }

    [JsonPropertyName("source_type")]
    public string SourceType { get; set; } = string.Empty;

    [JsonPropertyName("tags")]
    public List<string> Tags { get; set; } = new();

    [JsonPropertyName("tool_type")]
    public string ToolType { get; set; } = string.Empty;

    [JsonPropertyName("updated_at")]
    public DateTime UpdatedAt { get; set; }

    [JsonPropertyName("metadata_")]
    public Dictionary<string, object> Metadata { get; set; } = new();
} 