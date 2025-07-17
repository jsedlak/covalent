namespace Covalent.Agents.Model;

[GenerateSerializer]
[Alias("Message")]
public sealed class Message
{
    [Id(0)]
    public DateTime CreatedAt { get; set; }

    [Id(1)]
    public string? GroupId { get; set; }

    [Id(2)]
    public string Model { get; set; } = string.Empty;

    [Id(3)]
    public string? Name { get; set; }

    [Id(4)]
    public string Role { get; set; } = string.Empty;

    [Id(5)]
    public List<MessageContent> Content { get; set; } = new();

    [Id(6)]
    public string? ToolCallId { get; set; }

    [Id(7)]
    public List<ToolCall> ToolCalls { get; set; } = new();

    [Id(8)]
    public List<object> ToolReturns { get; set; } = new();

    [Id(9)]
    public DateTime UpdatedAt { get; set; }
} 