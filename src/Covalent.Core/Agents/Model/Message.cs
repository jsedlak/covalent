using System.Text.Json.Serialization;

namespace Covalent.Agents.Model;

[GenerateSerializer]
[Alias("Message")]
public sealed class Message
{
    [Id(0)]
    [JsonPropertyName("created_at")]
    public DateTime CreatedAt { get; set; }

    [Id(1)]
    [JsonPropertyName("group_id")]
    public string? GroupId { get; set; }

    [Id(2)]
    [JsonPropertyName("model")]
    public string Model { get; set; } = string.Empty;

    [Id(3)]
    [JsonPropertyName("name")]
    public string? Name { get; set; }

    [Id(4)]
    [JsonPropertyName("role")]
    public string Role { get; set; } = string.Empty;

    [Id(5)]
    [JsonPropertyName("content")]
    public List<MessageContent> Content { get; set; } = new();

    [Id(6)]
    [JsonPropertyName("tool_call_id")]
    public string? ToolCallId { get; set; }

    [Id(7)]
    [JsonPropertyName("tool_calls")]
    public List<ToolCall> ToolCalls { get; set; } = new();

    [Id(8)]
    [JsonPropertyName("tool_returns")]
    public List<object> ToolReturns { get; set; } = new();

    [Id(9)]
    [JsonPropertyName("updated_at")]
    public DateTime UpdatedAt { get; set; }
} 