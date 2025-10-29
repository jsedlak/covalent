using Covalent.Plugins.Storyblok.ServiceModel;
using Covalent.Plugins.Storyblok.Model;
using Microsoft.Extensions.Options;
using System.Text.Json;

namespace Covalent.Plugins.Storyblok.Services;

public class StoryblokManagementService : IStoryblokManagementService
{
    private readonly StoryblokOptions _options;
    private readonly HttpClient _httpClient;

    public StoryblokManagementService(
        IHttpClientFactory httpClientFactory,
        IOptions<StoryblokOptions> options)
    {
        _httpClient = httpClientFactory.CreateClient("storyblok");
        _options = options.Value;
    }

    public async Task<IEnumerable<Component>> GetComponents()
    {
        var response = await _httpClient.GetAsync($"{_options.ManagementApiUrl}/v1/spaces/{_options.SpaceId}/components");
        response.EnsureSuccessStatusCode();

        // the response will be an object with a single components 
        // property that is an array of components.

        var content = await response.Content.ReadAsStringAsync();
        var doc = JsonDocument.Parse(content);

        return JsonSerializer.Deserialize<IEnumerable<Component>>(
            doc.RootElement.GetProperty("components").GetRawText()
        ) ?? Array.Empty<Component>();
        
    }
}