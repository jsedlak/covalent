using System.Text.Json.Serialization;

namespace Covalent.Agents.Model;

[GenerateSerializer]
[Alias("CoreMemory")]
public sealed class CoreMemory
{
    [Id(0)]
    [JsonPropertyName("created_at")]
    public DateTime CreatedAt { get; set; }

    [Id(1)]
    [JsonPropertyName("description")]
    public string? Description { get; set; }

    [Id(2)]
    [JsonPropertyName("is_template")]
    public bool IsTemplate { get; set; }

    [Id(3)]
    [JsonPropertyName("label")]
    public string Label { get; set; } = string.Empty;

    [Id(4)]
    [JsonPropertyName("limit")]
    public int Limit { get; set; }

    [Id(5)]
    [JsonPropertyName("metadata_")]
    public Dictionary<string, object> Metadata { get; set; } = new();

    [Id(6)]
    [JsonPropertyName("template_name")]
    public string? TemplateName { get; set; }

    [Id(7)]
    [JsonPropertyName("updated_at")]
    public DateTime UpdatedAt { get; set; }

    [Id(8)]
    [JsonPropertyName("value")]
    public string Value { get; set; } = string.Empty;
} 