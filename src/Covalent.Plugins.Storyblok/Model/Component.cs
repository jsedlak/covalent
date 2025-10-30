using System.Text.Json.Serialization;

namespace Covalent.Plugins.Storyblok.Model;

public sealed class Component
{
    [JsonPropertyName("id")]
    public long Id { get; set; }

    [JsonPropertyName("name")]
    public string Name { get; set; } = string.Empty;

    [JsonPropertyName("display_name")]
    public string? DisplayName { get; set; }

    [JsonPropertyName("description")]
    public string? Description { get; set; }

    [JsonPropertyName("created_at")]
    public DateTimeOffset CreatedAt { get; set; }

    [JsonPropertyName("updated_at")]
    public DateTimeOffset UpdatedAt { get; set; }

    [JsonPropertyName("schema")]
    public ComponentSchema? Schema { get; set; }

    [JsonPropertyName("image")]
    public string? Image { get; set; }

    [JsonPropertyName("preview_field")]
    public string? PreviewField { get; set; }

    [JsonPropertyName("is_root")]
    public bool IsRoot { get; set; }

    [JsonPropertyName("preview_tmpl")]
    public string? PreviewTmpl { get; set; }

    [JsonPropertyName("is_nestable")]
    public bool IsNestable { get; set; }

    [JsonPropertyName("all_presets")]
    public List<Preset> AllPresets { get; set; } = new();

    [JsonPropertyName("real_name")]
    public string? RealName { get; set; }

    [JsonPropertyName("component_group_uuid")]
    public string? ComponentGroupUuid { get; set; }

    [JsonPropertyName("icon")]
    public string? Icon { get; set; }

    [JsonPropertyName("color")]
    public string? Color { get; set; }

    [JsonPropertyName("internal_tags_list")]
    public List<InternalTag> InternalTagsList { get; set; } = new();

    [JsonPropertyName("internal_tag_ids")]
    public List<string> InternalTagIds { get; set; } = new();

    [JsonPropertyName("content_type_asset_preview")]
    public string? ContentTypeAssetPreview { get; set; }
}