using Covalent.Plugins.Storyblok.ServiceModel;

namespace Covalent.Plugins.Storyblok.Services;

public class StoryblokManagementService : IStoryblokManagementService
{
    private readonly StoryblokOptions _options;
    private readonly HttpClient _httpClient;

    public StoryblokManagementService(
        [FromKeyedServices("storyblok")] HttpClient httpClient,
        StoryblokOptions options)
    {
        _httpClient = httpClient;
        _options = options;
    }
    
    public async Task<IEnumerable<Component>> GetComponents(string spaceId)
    {
        var response = await _httpClient.GetAsync($"{_options.ManagementApiUrl}/spaces/{spaceId}/components");
        response.EnsureSuccessStatusCode();
        return await response.Content.ReadFromJsonAsync<IEnumerable<Component>>();
    }
}