using Microsoft.AspNetCore.Mvc;

namespace Covalent.Silo.Controllers;

[ApiController]
[Route("api/[controller]")]
public sealed class AgentsController : ControllerBase
{
    private readonly IClusterClient _clusterClient;

    public AgentsController(IClusterClient clusterClient)
    {
        _clusterClient = clusterClient;
    }
}