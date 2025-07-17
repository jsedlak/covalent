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
    public string AgentType { get; set; } = string.Empty;

    [Id(3)]
    public List<CoreMemory> CoreMemory { get; set; } = new();

    [Id(4)]
    public DateTime CreatedAt { get; set; }

    [Id(5)]
    public EmbeddingConfig EmbeddingConfig { get; set; } = new();

    [Id(6)]
    public LlmConfig LlmConfig { get; set; } = new();

    [Id(7)]
    public bool MessageBufferAutoclear { get; set; }

    [Id(8)]
    public List<int> InContextMessageIndices { get; set; } = new();

    [Id(9)]
    public List<Message> Messages { get; set; } = new();

    [Id(10)]
    public Dictionary<string, object>? Metadata { get; set; }

    [Id(11)]
    public object? MultiAgentGroup { get; set; }

    [Id(12)]
    public string System { get; set; } = string.Empty;

    [Id(13)]
    public List<string> Tags { get; set; } = new();

    [Id(14)]
    public List<Dictionary<string, object>> ToolExecEnvironmentVariables { get; set; } = new();

    [Id(15)]
    public List<ToolRule> ToolRules { get; set; } = new();

    [Id(16)]
    public List<Tool> Tools { get; set; } = new();

    [Id(17)]
    public DateTime UpdatedAt { get; set; }

    [Id(18)]
    public string Version { get; set; } = string.Empty;
}