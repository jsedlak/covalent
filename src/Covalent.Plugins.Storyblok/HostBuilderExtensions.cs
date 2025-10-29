using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;

namespace Covalent;

public static class HostBuilderExtensions
{
    private const string DefaultSectionName = "Storyblok";
    
    /// <summary>
    /// Adds Storyblok services to the host builder using the default "Storyblok" configuration section.
    /// </summary>
    public static IHostApplicationBuilder AddStoryblok(this IHostApplicationBuilder builder)
    {
        return builder.AddStoryblok(DefaultSectionName);
    }
    
    /// <summary>
    /// Adds Storyblok services to the host builder using a specified configuration section name.
    /// </summary>
    public static IHostApplicationBuilder AddStoryblok(
        this IHostApplicationBuilder builder,
        string sectionName)
    {
        builder.Services.AddStoryblok(builder.Configuration, sectionName);
        return builder;
    }
    
    /// <summary>
    /// Adds Storyblok services to the host builder using a provided configuration section.
    /// </summary>
    public static IHostApplicationBuilder AddStoryblok(
        this IHostApplicationBuilder builder,
        IConfigurationSection configurationSection)
    {
        builder.Services.AddStoryblok(configurationSection);
        return builder;
    }
}

