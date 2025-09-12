# Product Requirements Document: Azure Foundry Aspire Integration

## Introduction/Overview

This feature adds seamless Azure Foundry integration to Covalent applications through .NET Aspire hosting extensions. The feature simplifies the setup process by providing a fluent API that allows developers to register Azure Foundry agent management services with a single method call, eliminating the need for manual service registration and configuration.

The primary goal is to streamline the developer experience when setting up Covalent applications with Azure Foundry as the agent management backend.

## Goals

1. Provide a simple, one-line integration method for Azure Foundry in Aspire applications
2. Automatically register AzureFoundryAgentManagementService as IAgentManagementService
3. Enable configuration through Aspire Parameter values using a fluent API
4. Maintain backward compatibility with existing Covalent agent management functionality

## User Stories

**Primary User Story:**
- As a developer, I want to add Azure Foundry support to my Covalent app with `AddAzureFoundry("foundry").WithExisting(uri, token)` so that I can quickly configure cloud-based agent management without manual service registration.

**Supporting User Stories:**
- As a developer, I want the Azure Foundry service to be automatically available for dependency injection so that I can use it throughout my application without additional setup.
- As a developer, I want to configure Azure Foundry using Aspire Parameter values so that I can manage different configurations across development, staging, and production environments.

## Functional Requirements

1. The system must create a new project `Covalent.Aspire.Hosting.AzureFoundry` in the dotnet solution.

2. The system must provide an `AddAzureFoundry(string name)` extension method for Aspire host builders.

3. The `AddAzureFoundry` method must return a fluent builder that supports a `WithExisting(string uri, string token)` method.

4. The system must automatically register `AzureFoundryAgentManagementService` as an implementation of `IAgentManagementService` when `AddAzureFoundry` is called.

5. The system must support configuration through Aspire Parameter values that are consumed by the Aspire startup class.

6. The URI and token parameters must be passed to the AzureFoundryAgentManagementService during registration.

7. The system must integrate with the existing `Covalent.Providers.AzureFoundry` project to leverage the existing service implementation.

8. The extension method must follow standard .NET Aspire hosting patterns and conventions.

## Non-Goals (Out of Scope)

1. Implementation details of the AzureFoundryAgentManagementService class
2. Additional NuGet package dependencies beyond existing Covalent packages
3. Authentication mechanisms beyond URI and token-based authentication
4. Migration tools for existing manual configurations
5. Azure Foundry service deployment or infrastructure setup
6. Custom configuration UI or management tools

## Design Considerations

The API design should follow established .NET Aspire patterns:

```csharp
// Example usage in Program.cs
var builder = DistributedApplication.CreateBuilder(args);

var azureFoundryUri = builder.AddParameter("AzureFoundryManagementUri", true);
var azureFoundryToken = builder.AddParameter("AzureFoundryManagementToken", true);

builder.AddAzureFoundry("foundry")
    .WithExisting(uri: azureFoundryUri, token: azureFoundryToken);
```

The fluent API should be intuitive and consistent with other Aspire hosting extensions.

## Technical Considerations

1. The new project should reference `Covalent.Providers.AzureFoundry` to access the existing service implementation.
2. Aspire Parameter names should follow conventional naming patterns (e.g., `AzureFoundryManagementUri`, `AzureFoundryManagementToken`).
3. The extension should integrate with Aspire's service discovery and configuration systems.
4. Error handling should provide clear messages for missing or invalid configuration values.
5. The implementation should support the standard Aspire resource lifecycle (creation, configuration, disposal).

## Success Metrics

1. **Developer Experience:** Developers can successfully add Azure Foundry integration with a single fluent method call.
2. **Functionality:** AzureFoundryAgentManagementService is automatically registered and available for dependency injection.
3. **Compatibility:** Existing Covalent agent management functionality continues to work without changes.
4. **Configuration:** URI and token can be successfully passed through Aspire Parameter values and consumed by the service.
5. **Integration:** The solution builds successfully and follows .NET Aspire hosting conventions.

## Open Questions

1. Should the extension support additional configuration options beyond URI and token?
2. Are there specific error handling requirements for invalid Azure Foundry connections?
3. Should the extension support health checks for the Azure Foundry service?
4. Do we need to support multiple Azure Foundry instances in a single application?