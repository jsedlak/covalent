using Covalent.Providers.Services;
using Microsoft.AspNetCore.Mvc;

namespace Covalent.Silo.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProvidersController : ControllerBase
{
    private readonly IServiceProvider _serviceProvider;

    public ProvidersController(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    [HttpGet(Name = "GetProviders")]
    public IEnumerable<object> Get()
    {
        return _serviceProvider
            .GetKeyedServices<IAgentManagementService>(KeyedService.AnyKey)
            .Select(s => new { name = s.Name, category = s.Category });
    }
}
