using System.Text.Json;
using Covalent.Agents.Model;
using Covalent.Agents.ServiceModel;

namespace Covalent.Agents.Services;

public sealed class AgentFileAgentImporter : IAgentImporter
{
    public async Task<AgentDefinition> ImportAgent(string agentName, Dictionary<string, string> properties)
    {
        var filename = properties["file"];

        if (string.IsNullOrWhiteSpace(filename))
        {
            throw new ArgumentException("file property is required");
        }
        
        var agentFile = await File.ReadAllTextAsync(filename);
        var agentDefinition = JsonSerializer.Deserialize<AgentDefinition>(agentFile);
        
        return agentDefinition ?? throw new Exception("Failed to deserialize agent file");
    }
}