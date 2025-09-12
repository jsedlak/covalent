using System.Text.Json.Serialization;

namespace Covalent.Agents.Model;

internal sealed class AgentDefinition 
{
    [JsonPropertyName("name")]
    public string Name { get; set; } = string.Empty;

    [JsonPropertyName("description")]
    public string Description { get; set; } = string.Empty;

     [JsonPropertyName("agent_type")]
    public string AgentType { get; set; } = string.Empty;

     [JsonPropertyName("core_memory")]
    public List<CoreMemory> CoreMemory { get; set; } = new();

     [JsonPropertyName("created_at")]
    public DateTime CreatedAt { get; set; }

     [JsonPropertyName("embedding_config")]
    public EmbeddingConfig EmbeddingConfig { get; set; } = new();

    [JsonPropertyName("llm_config")]
    public LlmConfig LlmConfig { get; set; } = new();

    
    [JsonPropertyName("message_buffer_autoclear")]
    public bool MessageBufferAutoclear { get; set; }

    
    [JsonPropertyName("in_context_message_indices")]
    public List<int> InContextMessageIndices { get; set; } = new();

    
    [JsonPropertyName("messages")]
    public List<Message> Messages { get; set; } = new();

    
    [JsonPropertyName("metadata_")]
    public Dictionary<string, object>? Metadata { get; set; }

    
    [JsonPropertyName("multi_agent_group")]
    public object? MultiAgentGroup { get; set; }

    
    [JsonPropertyName("system")]
    public string SystemPrompt { get; set; } = string.Empty;

    
    [JsonPropertyName("tags")]
    public List<string> Tags { get; set; } = new();

    
    [JsonPropertyName("tool_exec_environment_variables")]
    public List<Dictionary<string, object>> ToolExecEnvironmentVariables { get; set; } = new();

    
    [JsonPropertyName("tool_rules")]
    public List<ToolRule> ToolRules { get; set; } = new();

    
    [JsonPropertyName("tools")]
    public List<Tool> Tools { get; set; } = new();

    
    [JsonPropertyName("updated_at")]
    public DateTime UpdatedAt { get; set; }

    
    [JsonPropertyName("version")]
    public string Version { get; set; } = string.Empty;
}