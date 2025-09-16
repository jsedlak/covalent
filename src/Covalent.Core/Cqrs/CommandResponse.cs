namespace Covalent.Cqrs;

[GenerateSerializer]
public sealed class CommandResponse
{
    [Id(1)]
    public bool Success { get; set; }

    [Id(2)]
    public string[] Messages { get; set; } = Array.Empty<string>();

    public string CombinedMessages => string.Join(Environment.NewLine, Messages);
}
