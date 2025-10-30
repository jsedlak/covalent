using System.Text.Json;
using Covalent.Plugins.Storyblok.Model;

namespace Covalent.Plugins.Storyblok.Services;

public sealed class ComponentSerializer : IComponentSerializer
{
    private static readonly JsonSerializerOptions JsonOptions = new()
    {
        PropertyNameCaseInsensitive = true
    };

    public Component? Deserialize(string json)
    {
        if (string.IsNullOrWhiteSpace(json))
            return null;

        var doc = JsonDocument.Parse(json);
        var root = doc.RootElement;

        var component = new Component();

        // Deserialize standard properties
        if (root.TryGetProperty("id", out var idElement))
        {
            component.Id = idElement.GetInt64();
        }

        if (root.TryGetProperty("name", out var nameElement))
        {
            component.Name = nameElement.GetString() ?? string.Empty;
        }

        if (root.TryGetProperty("display_name", out var displayNameElement))
        {
            component.DisplayName = displayNameElement.GetString();
        }

        if (root.TryGetProperty("description", out var descriptionElement))
        {
            component.Description = descriptionElement.GetString();
        }

        if (root.TryGetProperty("created_at", out var createdAtElement))
        {
            component.CreatedAt = createdAtElement.GetDateTimeOffset();
        }

        if (root.TryGetProperty("updated_at", out var updatedAtElement))
        {
            component.UpdatedAt = updatedAtElement.GetDateTimeOffset();
        }

        // Deserialize schema if present
        if (root.TryGetProperty("schema", out var schemaElement) && schemaElement.ValueKind == JsonValueKind.Object)
        {
            component.Schema = DeserializeSchema(schemaElement);
        }

        return component;
    }

    public string Serialize(Component component)
    {
        using var stream = new System.IO.MemoryStream();
        using var writer = new Utf8JsonWriter(stream);
        
        writer.WriteStartObject();
        
        writer.WriteNumber("id", component.Id);
        writer.WriteString("name", component.Name);
        
        if (component.DisplayName != null)
        {
            writer.WriteString("display_name", component.DisplayName);
        }
        
        if (component.Description != null)
        {
            writer.WriteString("description", component.Description);
        }
        
        writer.WriteString("created_at", component.CreatedAt.ToString("O"));
        writer.WriteString("updated_at", component.UpdatedAt.ToString("O"));
        
        if (component.Schema != null)
        {
            writer.WritePropertyName("schema");
            SerializeSchema(writer, component.Schema);
        }
        
        writer.WriteEndObject();
        writer.Flush();
        
        return System.Text.Encoding.UTF8.GetString(stream.ToArray());
    }

    private ComponentSchema DeserializeSchema(JsonElement schemaElement)
    {
        var schema = new ComponentSchema();
        var allFields = new Dictionary<string, Field>();
        var sections = new List<Section>();
        var tabs = new List<Tab>();

        // First pass: collect all items from the schema dictionary
        foreach (var property in schemaElement.EnumerateObject())
        {
            var slug = property.Name;
            var value = property.Value;

            if (!value.TryGetProperty("type", out var typeElement))
            {
                continue;
            }

            var type = typeElement.GetString() ?? string.Empty;

            if (type == "section")
            {
                var section = JsonSerializer.Deserialize<Section>(value.GetRawText(), JsonOptions);
                if (section != null)
                {
                    section.Slug = slug;
                    sections.Add(section);
                }
            }
            else if (type == "tab")
            {
                var tab = JsonSerializer.Deserialize<Tab>(value.GetRawText(), JsonOptions);
                if (tab != null)
                {
                    tab.Slug = slug;
                    tabs.Add(tab);
                }
            }
            else
            {
                // It's a field
                var field = JsonSerializer.Deserialize<Field>(value.GetRawText(), JsonOptions);
                if (field != null)
                {
                    field.Slug = slug;
                    allFields[slug] = field;
                }
            }
        }

        // Second pass: associate fields with sections and tabs
        foreach (var section in sections)
        {
            foreach (var key in section.Keys)
            {
                if (allFields.TryGetValue(key, out var field))
                {
                    section.Fields.Add(field);
                }
            }
        }

        foreach (var tab in tabs)
        {
            foreach (var key in tab.Keys)
            {
                if (allFields.TryGetValue(key, out var field))
                {
                    tab.Fields.Add(field);
                }
            }
        }

        // Add standalone fields (not in sections or tabs)
        var fieldsInContainers = new HashSet<string>();
        foreach (var section in sections)
        {
            foreach (var key in section.Keys)
            {
                fieldsInContainers.Add(key);
            }
        }
        foreach (var tab in tabs)
        {
            foreach (var key in tab.Keys)
            {
                fieldsInContainers.Add(key);
            }
        }

        foreach (var field in allFields.Values)
        {
            if (field.Slug != null && !fieldsInContainers.Contains(field.Slug))
            {
                schema.Fields.Add(field);
            }
        }

        schema.Sections = sections;
        schema.Tabs = tabs;

        return schema;
    }

    private void SerializeSchema(Utf8JsonWriter writer, ComponentSchema schema)
    {
        writer.WriteStartObject();

        // Serialize sections
        foreach (var section in schema.Sections)
        {
            if (section.Slug != null)
            {
                writer.WritePropertyName(section.Slug);
                JsonSerializer.Serialize(writer, section, JsonOptions);
            }
        }

        // Serialize tabs
        foreach (var tab in schema.Tabs)
        {
            if (tab.Slug != null)
            {
                writer.WritePropertyName(tab.Slug);
                JsonSerializer.Serialize(writer, tab, JsonOptions);
            }
        }

        // Serialize standalone fields
        foreach (var field in schema.Fields)
        {
            if (field.Slug != null)
            {
                writer.WritePropertyName(field.Slug);
                JsonSerializer.Serialize(writer, field, JsonOptions);
            }
        }

        writer.WriteEndObject();
    }
}

