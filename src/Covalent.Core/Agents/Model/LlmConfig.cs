namespace Covalent.Agents.Model;

[GenerateSerializer]
[Alias("LlmConfig")]
public sealed class LlmConfig
{
    [Id(0)]
    public string Model { get; set; } = string.Empty;

    [Id(1)]
    public string ModelEndpointType { get; set; } = string.Empty;

    [Id(2)]
    public string ModelEndpoint { get; set; } = string.Empty;

    [Id(3)]
    public string? ModelWrapper { get; set; }

    [Id(4)]
    public int ContextWindow { get; set; }

    [Id(5)]
    public bool PutInnerThoughtsInKwargs { get; set; }

    [Id(6)]
    public string Handle { get; set; } = string.Empty;

    [Id(7)]
    public double Temperature { get; set; }

    [Id(8)]
    public int MaxTokens { get; set; }

    [Id(9)]
    public bool EnableReasoner { get; set; }

    [Id(10)]
    public int MaxReasoningTokens { get; set; }
} 