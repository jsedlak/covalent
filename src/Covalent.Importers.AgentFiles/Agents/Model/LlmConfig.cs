using System.Text.Json.Serialization;

namespace Covalent.Agents.Model;

internal sealed class LlmConfig
{
    [JsonPropertyName("model")]
    public string Model { get; set; } = string.Empty;

    [JsonPropertyName("model_endpoint_type")]
    public string ModelEndpointType { get; set; } = string.Empty;

    [JsonPropertyName("model_endpoint")]
    public string ModelEndpoint { get; set; } = string.Empty;

    [JsonPropertyName("model_wrapper")]
    public string? ModelWrapper { get; set; }

    [JsonPropertyName("context_window")]
    public int ContextWindow { get; set; }

    [JsonPropertyName("put_inner_thoughts_in_kwargs")]
    public bool PutInnerThoughtsInKwargs { get; set; }

    [JsonPropertyName("handle")]
    public string Handle { get; set; } = string.Empty;

    [JsonPropertyName("temperature")]
    public double Temperature { get; set; }

    [JsonPropertyName("max_tokens")]
    public int MaxTokens { get; set; }

    [JsonPropertyName("enable_reasoner")]
    public bool EnableReasoner { get; set; }

    [JsonPropertyName("max_reasoning_tokens")]
    public int MaxReasoningTokens { get; set; }
} 