using Covalent.Aspire.Hosting.Resources;

namespace Aspire.Hosting;

public static class AzureFoundryResourceBuilderExtensions
{
    public static IResourceBuilder<AzureFoundryResource> AddAzureFoundry(
        this IDistributedApplicationBuilder builder,
        string name)
    {
        var uriParameter = builder.AddParameter($"{name}ManagementUri", true);
        var tokenParameter = builder.AddParameter($"{name}AccessToken", true);

        var resource = new AzureFoundryResource(name)
        {
            UriParameter = uriParameter,
            TokenParameter = tokenParameter
        };

        return builder.AddResource(resource);
    }

    public static IResourceBuilder<AzureFoundryResource> AddAzureFoundry(
        this IDistributedApplicationBuilder builder,
        string name,
        string? managementUri,
        string? accessToken)
    {
        var resource = new AzureFoundryResource(name)
        {
            Uri = managementUri,
            Token = accessToken
        };

        return builder.AddResource(resource);
    }

    public static IResourceBuilder<TDestination> WithReference<TDestination>(
        this IResourceBuilder<TDestination> builder,
        IResourceBuilder<AzureFoundryResource> source) where TDestination : IResourceWithEnvironment
    {
        builder.WithEnvironment($"Covalent__Providers__AzureFoundry__{source.Resource.Name}", source.Resource.Name);

        if (source.Resource.Uri is not null)
        {
            builder.WithEnvironment($"AzureFoundry__{source.Resource.Name}__Uri", source.Resource.Uri);
        }
        else if (source.Resource.UriParameter is not null)
        {
            builder.WithEnvironment($"AzureFoundry__{source.Resource.Name}__Uri", source.Resource.UriParameter);
        }

        if( source.Resource.Token is not null)
        {
            builder.WithEnvironment($"AzureFoundry__{source.Resource.Name}__Token", source.Resource.Token);
        }
        else if (source.Resource.TokenParameter is not null)
        {
            builder.WithEnvironment($"AzureFoundry__{source.Resource.Name}__Token", source.Resource.TokenParameter);
        }

        return builder;
    }
}
