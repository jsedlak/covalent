# Domain Model

This document describes the core model of Covalent. Each model is controlled by one or more Orleans Grains. Top level models follow a CQRS + Event Sourcing pattern, where changes to the model are supported via Commands and Events.

## Providers

A Provider represents a model host and a set of available models and capabilities. For instance, Azure AI Foundry, OpenAI and Ollama are all Providers.

A Covalent instance may have more than one of the same Provider, differentiated by name, available models and/or capabilities.

### Properties

| Name   | Type    | Description                                          |
| ------ | ------- | ---------------------------------------------------- |
| Name   | string  | The unique identifier for the provider instance      |
| Models | Model[] | The list of available models and their capabilities. |

### Model

| Name         | Type     | Description                                                      |
| ------------ | -------- | ---------------------------------------------------------------- |
| Name         | string   | The unique identifier for the model                              |
| Capabilities | string[] | A tag list of capabilities that the model contains, e.g. "tools" |

## Agents

An Agent is a mechanism for connecting data, tools and a provider to form the concept of an point of interactivity, or a hook into the AI subsystem.

Agents are not directly interactable - they are deployed to endpoints via the Deployments model.

| Name     | Type   | Description                                                                     |
| -------- | ------ | ------------------------------------------------------------------------------- |
| Id       | Guid   | The unique identifier for the Agent                                             |
| Name     | string | A friendly name for the agent                                                   |
| Slug     | string | A developer centric name for the agent, defaults to a slug version of the name. |
| Provider | string | The underlying provider used to host this agent                                 |
| Model    | string | The underlying model used for generation.                                       |

## Deployments

A Deployment is a live, interactible version of an Agent, representing a snapshot / point-in-time configuration of that Agent.

Where-as an Agent might declare that it contains a tool, such as a mechanism for managing a memory, the Deployment will have references to that tool and contain memory (as used by the model).

Deployments are made available through API endpoints. A dynamic endpoint can pass messages into a Deployment.
