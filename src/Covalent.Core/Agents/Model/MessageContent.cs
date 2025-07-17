namespace Covalent.Agents.Model;

[GenerateSerializer]
[Alias("MessageContent")]
public sealed class MessageContent
{
    [Id(0)]
    public string Type { get; set; } = string.Empty;

    [Id(1)]
    public string Text { get; set; } = string.Empty;
} 