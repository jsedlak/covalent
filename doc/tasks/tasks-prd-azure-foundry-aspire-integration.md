## Relevant Files

- `src/Covalent.Aspire.Hosting.AzureFoundry/Covalent.Aspire.Hosting.AzureFoundry.csproj` - New project file for the Aspire extension library.
- `src/Covalent.Aspire.Hosting.AzureFoundry/Extensions/DistributedApplicationBuilderExtensions.cs` - Main extension methods for adding Azure Foundry to Aspire applications.
- `src/Covalent.Aspire.Hosting.AzureFoundry/Resources/AzureFoundryResource.cs` - Aspire resource definition for Azure Foundry service.
- `src/Covalent.Aspire.Hosting.AzureFoundry/Builders/AzureFoundryResourceBuilder.cs` - Fluent builder for configuring Azure Foundry resources.
- `src/Covalent.Aspire.Hosting.AzureFoundry/Configuration/AzureFoundryOptions.cs` - Configuration options class for Azure Foundry settings.
- `src/Covalent.Providers.AzureFoundry/Services/AzureFoundryAgentManagementService.cs` - Existing service implementation (may need modifications).
- `covalent.sln` - Solution file that needs to include the new project.

### Notes

- This is a .NET 9.0 project using standard C# conventions
- Follow existing Covalent project structure and naming patterns
- The new project should be placed in the "2. Aspire Injection" solution folder
- No unit tests are required based on the PRD scope

## Tasks

- [x] 1.0 Create Covalent.Aspire.Hosting.AzureFoundry project structure
  - [x] 1.1 Create project directory `src/Covalent.Aspire.Hosting.AzureFoundry`
  - [x] 1.2 Create `Covalent.Aspire.Hosting.AzureFoundry.csproj` with .NET 9.0 target framework
  - [x] 1.3 Add project reference to `Covalent.Providers.AzureFoundry`
  - [x] 1.4 Add necessary Aspire hosting package references
  - [x] 1.5 Create basic directory structure (Extensions, Resources, Builders, Configuration)

- [x] 2.0 Implement Azure Foundry Aspire extension methods
  - [x] 2.1 Create `DistributedApplicationBuilderExtensions.cs` with `AddAzureFoundry(string name)` method
  - [x] 2.2 Implement fluent builder pattern returning `AzureFoundryResourceBuilder`
  - [x] 2.3 Create `AzureFoundryResourceBuilder.cs` with `WithExisting(string uri, string token)` method
  - [x] 2.4 Ensure extension methods follow standard Aspire hosting conventions
  - [x] 2.5 Add XML documentation comments for all public methods

- [x] 3.0 Configure service registration and dependency injection
  - [x] 3.1 Create `AzureFoundryResource.cs` that implements Aspire resource interface
  - [x] 3.2 Implement service registration logic to register `AzureFoundryAgentManagementService` as `IAgentManagementService`
  - [x] 3.3 Configure URI and token parameter passing to the service constructor
  - [x] 3.4 Add support for Aspire Parameter values (ParameterResource) in `WithExisting` method
  - [x] 3.5 Implement proper resource lifecycle management (creation, configuration, disposal)

- [x] 4.0 Update solution file and project references
  - [x] 4.1 Add new project to `covalent.sln` in "2. Aspire Injection" solution folder
  - [x] 4.2 Configure project build settings for all platforms (Debug/Release, x64/x86/Any CPU)
  - [x] 4.3 Verify solution builds successfully with new project
  - [x] 4.4 Update any existing projects that may need to reference the new Aspire extension

- [ ] 5.0 Create supporting types and configuration classes
  - [x] 5.1 Create `AzureFoundryOptions.cs` configuration class for service settings
  - [x] 5.2 Implement proper error handling with descriptive messages for missing/invalid configuration
  - [x] 5.3 Add validation for required URI and token parameters
  - [x] 5.4 Ensure configuration follows Aspire Parameter naming conventions (AzureFoundryManagementUri, AzureFoundryManagementToken)
  - [ ] 5.5 Add support for optional configuration properties that may be needed in the future