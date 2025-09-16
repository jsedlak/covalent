using System.Text.Json;
using Covalent.Agents.Model;

namespace Covalent.Agents.Services;

public sealed class AgentFileAgentImporter : IAgentImporter
{
    public async Task ImportAgent(string agentName, Dictionary<string, string> properties)
    {
        if (!properties.TryGetValue("file", out var filename) || string.IsNullOrWhiteSpace(filename))
        {
            throw new ArgumentException("file property is required");
        }
        
        var agentFile = await File.ReadAllTextAsync(filename);
        var agentDefinition = JsonSerializer.Deserialize<AgentDefinition>(agentFile, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

        if(agentDefinition == null) 
        {
            throw new Exception("Failed to deserialize agent file");
        }

        agentDefinition.Name = agentName;

        // TODO: Do something with this agent definition, like saving it to a database or registering it in the system.
    }
}