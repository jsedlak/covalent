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

        // Register the plugin with Hitch and provide configuration values
        // Store the ParameterResource objects directly - GetEnvironmentExports will create proper references
        builder.WithPlugin("Service", "Storyblok", name, new Dictionary<string, object>
        {
            ["SpaceId"] = spaceId.Resource,
            ["ManagementApiUrl"] = managementApiUrl.Resource,
            ["PersonalAccessToken"] = personalAccessToken.Resource
        });

        return builder;
    }
}

