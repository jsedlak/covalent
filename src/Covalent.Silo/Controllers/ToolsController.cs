using Covalent.Agents.Providers;
using Microsoft.AspNetCore.Mvc;

namespace Covalent.Silo.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ToolsController : ControllerBase
{
    private readonly IServiceProvider _serviceProvider;

    public ToolsController(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    [HttpGet]
    public IEnumerable<object> Get()
    {
        return _serviceProvider
            .GetKeyedServices<IToolProvider>(KeyedService.AnyKey)
            .Select(s => new {
                id = s.Id,
                name = s.Name,
                description = s.Description,
                tools = s.GetTools()
            });
    }
}
