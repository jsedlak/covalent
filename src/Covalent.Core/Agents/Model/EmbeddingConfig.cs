using System.Text.Json.Serialization;

namespace Covalent.Agents.Model;

[GenerateSerializer]
[Alias("EmbeddingConfig")]
public sealed class EmbeddingConfig
{
    [Id(0)]
    [JsonPropertyName("embedding_endpoint_type")]
    public string EmbeddingEndpointType { get; set; } = string.Empty;

    [Id(1)]
    [JsonPropertyName("embedding_endpoint")]
    public string EmbeddingEndpoint { get; set; } = string.Empty;

    [Id(2)]
    [JsonPropertyName("embedding_model")]
    public string EmbeddingModel { get; set; } = string.Empty;

    [Id(3)]
    [JsonPropertyName("embedding_dim")]
    public int EmbeddingDim { get; set; }

    [Id(4)]
    [JsonPropertyName("embedding_chunk_size")]
    public int EmbeddingChunkSize { get; set; }

    [Id(5)]
    [JsonPropertyName("handle")]
    public string Handle { get; set; } = string.Empty;

    [Id(6)]
    [JsonPropertyName("azure_endpoint")]
    public string? AzureEndpoint { get; set; }

    [Id(7)]
    [JsonPropertyName("azure_version")]
    public string? AzureVersion { get; set; }

    [Id(8)]
    [JsonPropertyName("azure_deployment")]
    public string? AzureDeployment { get; set; }
} 