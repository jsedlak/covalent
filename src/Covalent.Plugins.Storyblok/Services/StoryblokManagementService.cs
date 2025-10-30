using Covalent.Plugins.Storyblok.ServiceModel;
using Covalent.Plugins.Storyblok.Model;
using Microsoft.Extensions.Options;
using System.Text.Json;

namespace Covalent.Plugins.Storyblok.Services;

public class StoryblokManagementService : IStoryblokManagementService
{
    private readonly StoryblokOptions _options;
    private readonly HttpClient _httpClient;
    private readonly IComponentSerializer _componentSerializer;

    public StoryblokManagementService(
        IHttpClientFactory httpClientFactory,
        IOptions<StoryblokOptions> options,
        IComponentSerializer componentSerializer)
        : this(httpClientFactory, options, componentSerializer, "storyblok")
    {
    }

    public StoryblokManagementService(
        IHttpClientFactory httpClientFactory,
        IOptions<StoryblokOptions> options,
        IComponentSerializer componentSerializer,
        string clientName)
    {
        _httpClient = httpClientFactory.CreateClient(clientName);
        _options = options.Value;
        _componentSerializer = componentSerializer;
    }

    public async Task<IEnumerable<Component>> GetComponents()
    {
        var response = await _httpClient.GetAsync($"{_options.ManagementApiUrl}/v1/spaces/{_options.SpaceId}/components");
        response.EnsureSuccessStatusCode();

        // the response will be an object with a single components 
        // property that is an array of components.

        var content = await response.Content.ReadAsStringAsync();
        var doc = JsonDocument.Parse(content);

        var componentsArray = doc.RootElement.GetProperty("components");
        var components = new List<Component>();

        foreach (var componentElement in componentsArray.EnumerateArray())
        {
            var componentJson = componentElement.GetRawText();
            var component = _componentSerializer.Deserialize(componentJson);
            if (component != null)
            {
                components.Add(component);
            }
        }

        return components;
    }
}