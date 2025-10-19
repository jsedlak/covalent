using Azure.AI.Inference;
using Covalent.Silo.ControllerModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.AI;

namespace Covalent.Silo.Controllers;

[ApiController]
[Route("api/[controller]")]
public sealed class AgentsController : ControllerBase
{
    private readonly ChatCompletionsClient _client;
    private readonly IChatClient _chatClient;

    public AgentsController(ChatCompletionsClient client, IChatClient chatClient)
    {
        _client = client;
        _chatClient = chatClient;
    }

    [HttpPost("chat")]
    public async IAsyncEnumerable<string> Chat([FromBody] ChatRequest request)
    {
       var result = await _client.CompleteStreamingAsync(new ChatCompletionsOptions
        {
            Messages =
            {
                new ChatRequestUserMessage(request.Message)
            },
            MaxTokens = 100
        });

        await foreach(var response in result)
        {
            yield return response.ContentUpdate;
        }
    }

    [HttpPost("chat2")]
    public async IAsyncEnumerable<string> Chat2([FromBody] ChatRequest request)
    {
        var result = _chatClient.GetStreamingResponseAsync(request.Message);

        await foreach (var response in result)
        {
            yield return response.Text;
        }
    }
}