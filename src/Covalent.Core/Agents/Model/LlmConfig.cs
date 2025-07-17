using System.Text.Json.Serialization;

namespace Covalent.Agents.Model;

[GenerateSerializer]
[Alias("LlmConfig")]
public sealed class LlmConfig
{
    [Id(0)]
    [JsonPropertyName("model")]
    public string Model { get; set; } = string.Empty;

    [Id(1)]
    [JsonPropertyName("model_endpoint_type")]
    public string ModelEndpointType { get; set; } = string.Empty;

    [Id(2)]
    [JsonPropertyName("model_endpoint")]
    public string ModelEndpoint { get; set; } = string.Empty;

    [Id(3)]
    [JsonPropertyName("model_wrapper")]
    public string? ModelWrapper { get; set; }

    [Id(4)]
    [JsonPropertyName("context_window")]
    public int ContextWindow { get; set; }

    [Id(5)]
    [JsonPropertyName("put_inner_thoughts_in_kwargs")]
    public bool PutInnerThoughtsInKwargs { get; set; }

    [Id(6)]
    [JsonPropertyName("handle")]
    public string Handle { get; set; } = string.Empty;

    [Id(7)]
    [JsonPropertyName("temperature")]
    public double Temperature { get; set; }

    [Id(8)]
    [JsonPropertyName("max_tokens")]
    public int MaxTokens { get; set; }

    [Id(9)]
    [JsonPropertyName("enable_reasoner")]
    public bool EnableReasoner { get; set; }

    [Id(10)]
    [JsonPropertyName("max_reasoning_tokens")]
    public int MaxReasoningTokens { get; set; }
} 