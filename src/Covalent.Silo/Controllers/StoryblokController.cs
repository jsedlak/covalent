using Covalent.Plugins.Storyblok.Model;
using Covalent.Plugins.Storyblok.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;

namespace Covalent.Silo.Controllers;

[ApiController]
[Route("api/[controller]")]
public class StoryblokController : ControllerBase
{
    private readonly IStoryblokManagementService _storyblokManagementService;
    
    public StoryblokController([FromKeyedServices("storyblok")] IStoryblokManagementService storyblokManagementService)
    {
        _storyblokManagementService = storyblokManagementService;
    }

    [HttpGet("components")]
    public async Task<IEnumerable<Component>> GetComponents()
    {
        return await _storyblokManagementService.GetComponents();
    }

    [HttpGet("components/{componentId}")]
    public async Task<Component> GetComponent([FromRoute] long componentId)
    {
        return await _storyblokManagementService.GetComponent(componentId);
    }

    [HttpPost("components")]
    public async Task<Component> CreateComponent([FromBody] Component component)
    {
        return await _storyblokManagementService.CreateComponent(component);
    }
}