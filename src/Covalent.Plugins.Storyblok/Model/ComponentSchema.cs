namespace Covalent.Plugins.Storyblok.Model;

public sealed class ComponentSchema
{
    public List<Section> Sections { get; set; } = new();

    public List<Tab> Tabs { get; set; } = new();
    
    public List<Field> Fields { get; set; } = new();
}

