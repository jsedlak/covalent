using System.Text.Json.Serialization;

namespace Covalent.Plugins.Storyblok.Model;

public sealed class InternalTag
{
    [JsonPropertyName("id")]
    public long Id { get; set; }

    [JsonPropertyName("name")]
    public string Name { get; set; } = string.Empty;
}

