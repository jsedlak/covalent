using Covalent.Agents.Events;
using Covalent.Core.Cqrs;

namespace Covalent.Silos.Agents.Model;

/// <summary>
/// Represents an AI agent aggregate that manages agent configuration and behavior.
/// This class handles various agent-related events and maintains the agent's state
/// including metadata, provider settings, and system prompts.
/// </summary>
public class Agent : Aggregate
{
    /// <summary>
    /// Handles the AgentMetaSetEvent to update the agent's basic metadata.
    /// </summary>
    /// <param name="event">The event containing the agent's metadata information.</param>
    public void Handle(AgentMetaSetEvent @event) 
    {
        Id = @event.AggregateId;
        Name = @event.Name;
        Description = @event.Description;
    }

    /// <summary>
    /// Handles the AgentProviderAndModelSetEvent to update the agent's AI provider and model configuration.
    /// </summary>
    /// <param name="event">The event containing the provider and model information.</param>
    public void Handle(AgentProviderAndModelSetEvent @event)
    {
        Provider = @event.Provider;
        Model = @event.Model;
    }

    /// <summary>
    /// Handles the AgentSystemPromptSetEvent to update the agent's system prompt.
    /// </summary>
    /// <param name="event">The event containing the system prompt information.</param>
    public void Handle(AgentSystemPromptSetEvent @event)
    {
        SystemPrompt = @event.SystemPrompt;
    }

    /// <summary>
    /// Gets or sets the name of the agent.
    /// </summary>
    public string Name { get; set; } = null!;

    /// <summary>
    /// Gets or sets the description of the agent.
    /// </summary>
    public string Description { get; set; } = null!;

    /// <summary>
    /// Gets or sets the system prompt that defines the agent's behavior and personality.
    /// </summary>
    public string SystemPrompt { get; set; } = null!;

    /// <summary>
    /// Gets or sets the AI provider used by the agent (e.g., OpenAI, Anthropic, etc.).
    /// </summary>
    public string Provider { get; set; } = null!;

    /// <summary>
    /// Gets or sets the specific AI model used by the agent (e.g., gpt-4, claude-3, etc.).
    /// </summary>
    public string Model { get; set; } = null!;
}