using System.Text.Json.Serialization;

namespace Covalent.Agents.Model;

internal sealed class EmbeddingConfig
{
    [JsonPropertyName("embedding_endpoint_type")]
    public string EmbeddingEndpointType { get; set; } = string.Empty;

    [JsonPropertyName("embedding_endpoint")]
    public string EmbeddingEndpoint { get; set; } = string.Empty;

    [JsonPropertyName("embedding_model")]
    public string EmbeddingModel { get; set; } = string.Empty;

    [JsonPropertyName("embedding_dim")]
    public int EmbeddingDim { get; set; }

    [JsonPropertyName("embedding_chunk_size")]
    public int EmbeddingChunkSize { get; set; }

    [JsonPropertyName("handle")]
    public string Handle { get; set; } = string.Empty;

    [JsonPropertyName("azure_endpoint")]
    public string? AzureEndpoint { get; set; }

    [JsonPropertyName("azure_version")]
    public string? AzureVersion { get; set; }

    [JsonPropertyName("azure_deployment")]
    public string? AzureDeployment { get; set; }
} 