namespace Covalent.Cqrs;

[GenerateSerializer]
public sealed class CommandResponse
{
    public static CommandResponse Good() => new(true, Array.Empty<string>());

    public static CommandResponse Bad(string message) => new(false, new[] { message });

    public CommandResponse(bool success, string[] messages)
    {
        Success = success;
        Messages = messages;
    }

    [Id(1)]
    public bool Success { get; set; }

    [Id(2)]
    public string[] Messages { get; set; } = Array.Empty<string>();

    public string CombinedMessages => string.Join(Environment.NewLine, Messages);
}
