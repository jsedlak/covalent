using Covalent.Agents.Providers;
using Covalent.Plugins.Storyblok.Builders;
using Covalent.Plugins.Storyblok.Tools;
using Hitch;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

[assembly: HitchPlugin("Tool", "Storyblok", typeof(StoryblokToolProviderBuilder))]

namespace Covalent.Plugins.Storyblok.Builders;

internal class StoryblokToolProviderBuilder : IPluginProvider
{
    public void Attach(IServiceCollection services, IConfigurationSection configurationSection, string? name = null)
    {
        services.AddKeyedSingleton<IToolProvider, ManagementToolProvider>(name);
    }
}
