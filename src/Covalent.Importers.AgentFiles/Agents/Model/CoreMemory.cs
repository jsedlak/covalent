using System.Text.Json.Serialization;

namespace Covalent.Agents.Model;

internal sealed class CoreMemory
{
    [JsonPropertyName("created_at")]
    public DateTime CreatedAt { get; set; }

    [JsonPropertyName("description")]
    public string? Description { get; set; }

    [JsonPropertyName("is_template")]
    public bool IsTemplate { get; set; }

    [JsonPropertyName("label")]
    public string Label { get; set; } = string.Empty;

    [JsonPropertyName("limit")]
    public int Limit { get; set; }

    [JsonPropertyName("metadata_")]
    public Dictionary<string, object> Metadata { get; set; } = new();

    [JsonPropertyName("template_name")]
    public string? TemplateName { get; set; }

    [JsonPropertyName("updated_at")]
    public DateTime UpdatedAt { get; set; }

    [JsonPropertyName("value")]
    public string Value { get; set; } = string.Empty;
} 