using Covalent.Plugins.Storyblok.Model;

namespace Covalent.Plugins.Storyblok.Services;

public interface IComponentSerializer
{
    Component? Deserialize(string json);
    string Serialize(Component component);
}