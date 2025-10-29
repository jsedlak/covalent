namespace Hitch.Aspire.Hosting;

using global::Aspire.Hosting;
using global::Aspire.Hosting.ApplicationModel;

/// <summary>
/// Extension methods for <see cref="IHitchResourceBuilder"/> to add Storyblok Management configuration.
/// </summary>
public static class StoryblokHitchResourceBuilderExtensions
{
    /// <summary>
    /// Adds a Storyblok Management plugin instance to the Hitch resource.
    /// </summary>
    /// <param name="builder">The Hitch resource builder.</param>
    /// <param name="name">The name of this Storyblok instance (used as the service key).</param>
    /// <param name="spaceId">The Storyblok space ID parameter.</param>
    /// <param name="managementApiUrl">The Storyblok Management API URL parameter.</param>
    /// <param name="personalAccessToken">The Storyblok personal access token parameter.</param>
    /// <returns>The resource builder for chaining.</returns>
    public static IHitchResourceBuilder WithStoryblokManagement(
        this IHitchResourceBuilder builder,
        string name,
        IResourceBuilder<ParameterResource> spaceId,
        IResourceBuilder<ParameterResource> managementApiUrl,
        IResourceBuilder<ParameterResource> personalAccessToken)
    {
        if (builder == null)
        {
            throw new ArgumentNullException(nameof(builder));
        }

        if (string.IsNullOrWhiteSpace(name))
        {
            throw new ArgumentException("Name cannot be null or whitespace.", nameof(name));
        }

        if (spaceId == null)
        {
            throw new ArgumentNullException(nameof(spaceId));
        }

        if (managementApiUrl == null)
        {
            throw new ArgumentNullException(nameof(managementApiUrl));
        }

        if (personalAccessToken == null)
        {
            throw new ArgumentNullException(nameof(personalAccessToken));
        }

        // Register the plugin with Hitch
        builder.WithPlugin("Tool", "Storyblok", name);

        // Store the configuration values for this instance
        var configKey = $"Tool__Storyblok__{name}";
        if (!builder.Resource.PluginConfigurations.ContainsKey(configKey))
        {
            builder.Resource.PluginConfigurations[configKey] = new Dictionary<string, object>();
        }

        var config = builder.Resource.PluginConfigurations[configKey];
        
        // Store as ReferenceExpression for proper parameter resolution
        config["SpaceId"] = ReferenceExpression.Create($"{spaceId.Resource.Name}");
        config["ManagementApiUrl"] = ReferenceExpression.Create($"{managementApiUrl.Resource.Name}");
        config["PersonalAccessToken"] = ReferenceExpression.Create($"{personalAccessToken.Resource.Name}");

        return builder;
    }
}

