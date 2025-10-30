using System.Text.Json;
using System.Text.Json.Serialization;
using Covalent.Plugins.Storyblok.Model;

namespace Covalent.Plugins.Storyblok.Services;

public sealed class ComponentSerializer : IComponentSerializer
{
    private static readonly JsonSerializerOptions JsonOptions = new()
    {
        PropertyNameCaseInsensitive = true,
        Converters = { new ComponentSchemaJsonConverter() },
        DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull
    };

    public Component? Deserialize(string json)
    {
        if (string.IsNullOrWhiteSpace(json))
            return null;

        return JsonSerializer.Deserialize<Component>(json, JsonOptions);
    }

    public string Serialize(Component component)
    {
        return JsonSerializer.Serialize(component, JsonOptions);
    }
}

