using Covalent.Plugins.Storyblok.ServiceModel;
using Covalent.Plugins.Storyblok.Model;

namespace Covalent.Plugins.Storyblok.Services;

public interface IStoryblokManagementService
{
    Task<IEnumerable<Component>> GetComponents();
}

