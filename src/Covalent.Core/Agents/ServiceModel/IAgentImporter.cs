using Covalent.Agents.Model;

namespace Covalent.Agents.ServiceModel;

public interface IAgentImporter 
{
    Task<AgentDefinition> ImportAgent(string agentName, Dictionary<string, string> properties);
}