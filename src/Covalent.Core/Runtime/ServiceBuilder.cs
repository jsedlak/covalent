using System;
using Microsoft.Extensions.Hosting;

namespace Covalent.Runtime;

public abstract class ServiceBuilder
{
    public abstract void Register(IHostApplicationBuilder builder, string name);
}
