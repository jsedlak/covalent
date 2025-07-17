using System.Text.Json.Serialization;

namespace Covalent.Agents.Model;

[GenerateSerializer]
[Alias("AgentDefinition")]
public sealed class AgentDefinition 
{
    [Id(0)]
    public string Name { get; set; } = string.Empty;

    [Id(1)]
    public string Description { get; set; } = string.Empty;

    [Id(2)]
    [JsonPropertyName("agent_type")]
    public string AgentType { get; set; } = string.Empty;

    [Id(3)]
    [JsonPropertyName("core_memory")]
    public List<CoreMemory> CoreMemory { get; set; } = new();

    [Id(4)]
    [JsonPropertyName("created_at")]
    public DateTime CreatedAt { get; set; }

    [Id(5)]
    [JsonPropertyName("embedding_config")]
    public EmbeddingConfig EmbeddingConfig { get; set; } = new();

    [Id(6)]
    [JsonPropertyName("llm_config")]
    public LlmConfig LlmConfig { get; set; } = new();

    [Id(7)]
    [JsonPropertyName("message_buffer_autoclear")]
    public bool MessageBufferAutoclear { get; set; }

    [Id(8)]
    [JsonPropertyName("in_context_message_indices")]
    public List<int> InContextMessageIndices { get; set; } = new();

    [Id(9)]
    [JsonPropertyName("messages")]
    public List<Message> Messages { get; set; } = new();

    [Id(10)]
    [JsonPropertyName("metadata_")]
    public Dictionary<string, object>? Metadata { get; set; }

    [Id(11)]
    [JsonPropertyName("multi_agent_group")]
    public object? MultiAgentGroup { get; set; }

    [Id(12)]
    [JsonPropertyName("system")]
    public string System { get; set; } = string.Empty;

    [Id(13)]
    [JsonPropertyName("tags")]
    public List<string> Tags { get; set; } = new();

    [Id(14)]
    [JsonPropertyName("tool_exec_environment_variables")]
    public List<Dictionary<string, object>> ToolExecEnvironmentVariables { get; set; } = new();

    [Id(15)]
    [JsonPropertyName("tool_rules")]
    public List<ToolRule> ToolRules { get; set; } = new();

    [Id(16)]
    [JsonPropertyName("tools")]
    public List<Tool> Tools { get; set; } = new();

    [Id(17)]
    [JsonPropertyName("updated_at")]
    public DateTime UpdatedAt { get; set; }

    [Id(18)]
    [JsonPropertyName("version")]
    public string Version { get; set; } = string.Empty;
}