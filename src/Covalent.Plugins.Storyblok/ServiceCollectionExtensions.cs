using Covalent.Plugins.Storyblok.ServiceModel;
using Covalent.Plugins.Storyblok.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace Covalent;

public static class ServiceCollectionExtensions
{
    private const string DefaultSectionName = "Storyblok";
    
    /// <summary>
    /// Adds Storyblok services to the service collection using the default "Storyblok" configuration section.
    /// </summary>
    public static IServiceCollection AddStoryblok(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        return services.AddStoryblok(configuration, DefaultSectionName);
    }
    
    /// <summary>
    /// Adds Storyblok services to the service collection using a specified configuration section name.
    /// </summary>
    public static IServiceCollection AddStoryblok(
        this IServiceCollection services,
        IConfiguration configuration,
        string sectionName)
    {
        var section = configuration.GetSection(sectionName);
        return services.AddStoryblok(section);
    }
    
    /// <summary>
    /// Adds Storyblok services to the service collection using a provided configuration section.
    /// </summary>
    public static IServiceCollection AddStoryblok(
        this IServiceCollection services,
        IConfigurationSection configurationSection)
    {
        // Configure options from configuration
        services.Configure<StoryblokOptions>(configurationSection);
        services.Configure<StoryblokTokenOptions>(configurationSection);
        
        // Add keyed HttpClient with authorization header
        services.AddHttpClient(
            "storyblok",
            (serviceProvider, client) =>
            {
                var token = serviceProvider.GetRequiredService<IOptions<StoryblokTokenOptions>>();
                client.DefaultRequestHeaders.Authorization = 
                    new System.Net.Http.Headers.AuthenticationHeaderValue(token.Value.PersonalAccessToken);
            });
        
        // Register the service
        services.AddScoped<IStoryblokManagementService, StoryblokManagementService>();
        
        return services;
    }
}

