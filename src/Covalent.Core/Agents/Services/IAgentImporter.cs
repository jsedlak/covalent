
namespace Covalent.Agents.Services;

public interface IAgentImporter 
{
    Task ImportAgent(string agentName, Dictionary<string, string> properties);
}