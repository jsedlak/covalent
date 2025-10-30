using System.Text.Json.Serialization;

namespace Covalent.Plugins.Storyblok.Model;

public sealed class Field
{
    [JsonPropertyName("id")]
    public string? Id { get; set; }

    [JsonPropertyName("name")]
    public string? Name { get; set; }

    [JsonPropertyName("description")]
    public string? Description { get; set; }

    [JsonPropertyName("slug")]
    public string? Slug { get; set; }

    [JsonPropertyName("type")]
    public string Type { get; set; } = string.Empty;

    [JsonPropertyName("pos")]
    public int? Pos { get; set; }

    [JsonPropertyName("is_reference_type")]
    public bool? IsReferenceType { get; set; }

    [JsonPropertyName("source")]
    public string? Source { get; set; }

    [JsonPropertyName("entry_appearance")]
    public string? EntryAppearance { get; set; }

    [JsonPropertyName("allow_advanced_search")]
    public bool? AllowAdvancedSearch { get; set; }
}
