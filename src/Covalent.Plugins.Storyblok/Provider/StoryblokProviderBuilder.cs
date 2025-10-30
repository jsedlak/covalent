using Covalent.Plugins.Storyblok.Provider;
using Hitch;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

[assembly: HitchPlugin("Service", "Storyblok", typeof(StoryblokProviderBuilder))]

namespace Covalent.Plugins.Storyblok.Provider;

internal class StoryblokProviderBuilder : IPluginProvider
{
    public void Attach(IServiceCollection services, IConfigurationSection configurationSection, string? name = null)
    {
        services.AddStoryblok(configurationSection, name);
    }
}
