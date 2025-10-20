using Azure.AI.Inference;
using Covalent.Silo.ControllerModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.AI;
using System.Text.Json;

namespace Covalent.Silo.Controllers;

[ApiController]
[Route("api/[controller]")]
public sealed class AgentsController : ControllerBase
{
    private readonly ChatCompletionsClient _client;
    private readonly IChatClient _chatClient;
    private ILogger<AgentsController> _logger;

    public AgentsController(ChatCompletionsClient client, IChatClient chatClient, ILogger<AgentsController> logger)
    {
        _client = client;
        _chatClient = chatClient;
        _logger = logger;
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
    public async Task Chat2([FromBody] ChatRequest request)
    {
        var result = _chatClient.GetStreamingResponseAsync(request.Message);

        Response.Headers["Content-Encoding"] = "identity";
        Response.Headers.Append("Cache-Control", "no-cache");
        Response.Headers.Append("X-Accel-Buffering", "no");

        Response.ContentType = "text/plain";
        
        await foreach (var response in result)
        {
            _logger.LogInformation(JsonSerializer.Serialize(response));

            await Response.WriteAsync(response.Text);
            await Response.Body.FlushAsync();
        }
    }
}