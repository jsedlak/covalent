namespace Covalent.Agents.Model;

[GenerateSerializer]
[Alias("EmbeddingConfig")]
public sealed class EmbeddingConfig
{
    [Id(0)]
    public string EmbeddingEndpointType { get; set; } = string.Empty;

    [Id(1)]
    public string EmbeddingEndpoint { get; set; } = string.Empty;

    [Id(2)]
    public string EmbeddingModel { get; set; } = string.Empty;

    [Id(3)]
    public int EmbeddingDim { get; set; }

    [Id(4)]
    public int EmbeddingChunkSize { get; set; }

    [Id(5)]
    public string Handle { get; set; } = string.Empty;

    [Id(6)]
    public string? AzureEndpoint { get; set; }

    [Id(7)]
    public string? AzureVersion { get; set; }

    [Id(8)]
    public string? AzureDeployment { get; set; }
} 