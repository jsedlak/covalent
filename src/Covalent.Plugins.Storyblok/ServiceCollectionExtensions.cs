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
        IConfigurationSection configurationSection,
        string? name = null)
    {
        // Register ComponentSerializer as singleton
        services.AddSingleton<IComponentSerializer, ComponentSerializer>();

        if (string.IsNullOrEmpty(name))
        {
            // Non-keyed registration for backward compatibility
            services.Configure<StoryblokOptions>(configurationSection);
            services.Configure<StoryblokTokenOptions>(configurationSection);
            
            // Add HttpClient with authorization header
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
        }
        else
        {
            // Keyed registration with name
            // Bind options from configuration section
            var storyblokOptions = new StoryblokOptions();
            var storyblokTokenOptions = new StoryblokTokenOptions();
            configurationSection.Bind(storyblokOptions);
            configurationSection.Bind(storyblokTokenOptions);
            
            // Register as keyed singletons for options
            services.AddKeyedSingleton(name, storyblokOptions);
            services.AddKeyedSingleton(name, storyblokTokenOptions);
            
            // Add keyed HttpClient with authorization header
            services.AddHttpClient(
                name,
                (serviceProvider, client) =>
                {
                    var token = serviceProvider.GetRequiredKeyedService<StoryblokTokenOptions>(name);
                    client.DefaultRequestHeaders.Authorization = 
                        new System.Net.Http.Headers.AuthenticationHeaderValue(token.PersonalAccessToken);
                });
            
            // Register the service as keyed
            services.AddKeyedScoped<IStoryblokManagementService>(
                name,
                (serviceProvider, key) =>
                {
                    var httpClientFactory = serviceProvider.GetRequiredService<IHttpClientFactory>();
                    var options = serviceProvider.GetRequiredKeyedService<StoryblokOptions>(key);
                    var componentSerializer = serviceProvider.GetRequiredService<IComponentSerializer>();
                    return new StoryblokManagementService(httpClientFactory, Microsoft.Extensions.Options.Options.Create(options), componentSerializer, name);
                });
        }
        
        return services;
    }
}

