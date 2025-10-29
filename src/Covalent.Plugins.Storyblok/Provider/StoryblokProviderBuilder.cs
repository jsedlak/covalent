using Covalent.Plugins.Storyblok.Provider;
using Hitch;
using Microsoft.Extensions.DependencyInjection;

[assembly: HitchPlugin("Content", "Storyblok", typeof(StoryblokProviderBuilder))]

namespace Covalent.Plugins.Storyblok.Provider;

internal class StoryblokProviderBuilder : IPluginProvider
{
    public void Attach(IServiceCollection services, string? name = null)
    {
        services.AddStoryblok();
    }
}
