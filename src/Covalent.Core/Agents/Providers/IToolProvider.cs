using Covalent.Agents.Model;

namespace Covalent.Agents.Providers;

public interface IToolProvider
{
    IEnumerable<Tool> GetTools();

    string Id { get; }

    string? Icon { get; }

    string Name { get; }

    string Description { get; }

}
