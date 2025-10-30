using System.Text.Json.Serialization;

namespace Covalent.Plugins.Storyblok.Model;

public sealed class Tab
{
    [JsonPropertyName("id")]
    public string? Id { get; set; }

    [JsonPropertyName("name")]
    public string? Name { get; set; }

    [JsonPropertyName("description")]
    public string? Description { get; set; }

    [JsonPropertyName("slug")]
    public string? Slug { get; set; }

    [JsonPropertyName("display_name")]
    public string? DisplayName { get; set; }

    [JsonPropertyName("type")]
    public string Type { get; set; } = string.Empty;

    [JsonPropertyName("pos")]
    public int? Pos { get; set; }

    [JsonPropertyName("keys")]
    public List<string> Keys { get; set; } = new();

    public List<Field> Fields { get; set; } = new();
}

