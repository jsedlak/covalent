# Hitch Multi-Instance Plugin Support - Implementation Summary

## Overview

Successfully implemented support for multiple named instances of Hitch plugins with keyed services, enabling configuration like:

```csharp
var hitch = builder.AddHitch(hitchBuilder => hitchBuilder.WithFilePattern("Covalent.Plugins.*.dll"))
    .WithStoryblokManagement("storyblok", spaceId, apiUrl, token)
    .WithStoryblokManagement("space2", spaceId2, apiUrl2, token2);
```

The implementation uses **individual environment variables** (not JSON) to pass configuration from the AppHost to the Silo:

```
HITCH__PLUGINS__TOOL__STORYBLOK__storyblok = "storyblok"
HITCH__PLUGINS__TOOL__STORYBLOK__storyblok__SpaceId = "{parameter-value}"
HITCH__PLUGINS__TOOL__STORYBLOK__storyblok__ManagementApiUrl = "{parameter-value}"
HITCH__PLUGINS__TOOL__STORYBLOK__storyblok__PersonalAccessToken = "{parameter-value}"
```

## Changes Made

### 1. Core Hitch Changes

#### `hitch/src/Hitch/IPluginProvider.cs`

- Updated `Attach` method signature to accept `IConfigurationSection` parameter
- Plugins now receive their configuration section directly:
  ```csharp
  void Attach(IServiceCollection services, IConfigurationSection configurationSection, string? name = null);
  ```

#### `hitch/src/Hitch/HitchBuilder.cs`

- Modified `ProcessCategorizedPlugin` to iterate through named instance sections
- **Supports two configuration formats for backward compatibility:**
  - **New format**: `Hitch:Plugins:Category:SubCategory:serviceName = "serviceName"` (key is service name)
  - **Old format**: `Hitch:Plugins:Category:SubCategory:0 = "serviceName"` (array-style, value is service name)
- Updated `AttachPlugin` to construct configuration section path: `Hitch:Plugins:{Category}:{SubCategory}:{name}`
- Plugins are discovered by scanning child sections under each category/subcategory

#### `hitch/src/Hitch.Aspire.Hosting/HitchResource.cs`

- Added `PluginConfigurations` dictionary to store plugin instance configurations
- Stores configuration values keyed by `Category__SubCategory__ServiceName`
- Updated `GetEnvironmentExports` to return `IEnumerable<(string Key, object Value)>`
- Exports individual environment variables for each plugin instance:
  - Plugin registration: `Hitch__Plugins__{Category}__{SubCategory}__{serviceName} = serviceName`
  - Plugin configuration: `Hitch__Plugins__{Category}__{SubCategory}__{serviceName}__{PropertyName} = value`
- Supports Aspire `ReferenceExpression` for parameter resolution

#### `hitch/src/Hitch.Aspire.Hosting/HitchResourceBuilderExtensions.cs`

- **Removed** `PublishAsConfiguration` method (didn't work correctly with Aspire's configuration system)
- Updated `WithReference` to use `GetEnvironmentExports` from HitchResource
- Environment variables are now injected via `WithEnvironment` callback when consumer calls `.WithReference(hitch)`
- Each plugin instance gets its own set of environment variables

#### `hitch/src/Hitch.Aspire.Hosting/DistributedApplicationExtensions.cs`

- Removed automatic call to `PublishAsConfiguration`
- Configuration is now exported automatically through `WithReference`

### 2. New Aspire Hosting Project

#### `src/Covalent.Aspire.Hosting.Storyblok/`

- Created new project for Storyblok-specific Aspire hosting extensions
- Added to solution in "2. Aspire Injection" folder
- References `Hitch.Aspire.Hosting` and `Aspire.Hosting`

#### `src/Covalent.Aspire.Hosting.Storyblok/HitchResourceBuilderExtensions.cs`

- Implemented `WithStoryblokManagement` extension method
- Accepts Aspire parameter resources for SpaceId, ManagementApiUrl, and PersonalAccessToken
- Registers plugin with Hitch using `WithPlugin("Tool", "Storyblok", name)`
- Stores configuration in `PluginConfigurations` as `ReferenceExpression` objects for proper parameter resolution

### 3. Storyblok Plugin Updates

#### `src/Covalent.Plugins.Storyblok/Provider/StoryblokProviderBuilder.cs`

- Updated `Attach` method to use new signature with `IConfigurationSection`
- Calls `AddStoryblok(configurationSection, name)` to register services

#### `src/Covalent.Plugins.Storyblok/ServiceCollectionExtensions.cs`

- Added support for keyed service registration when `name` parameter is provided
- Non-keyed registration maintained for backward compatibility (when name is null)
- Keyed services registered:
  - `StoryblokOptions` as keyed singleton
  - `StoryblokTokenOptions` as keyed singleton
  - `IStoryblokManagementService` as keyed scoped service
- Each named instance uses its own HttpClient with proper authentication

#### `src/Covalent.Plugins.Storyblok/Services/StoryblokManagementService.cs`

- Added constructor overload accepting `clientName` parameter
- Enables proper HttpClient resolution for each named instance

#### `src/Covalent.Plugins.Storyblok/Covalent.Plugins.Storyblok.csproj`

- Added project reference to Hitch

### 4. Consumer Updates

#### `src/Covalent.Silo/Controllers/StoryblokController.cs`

- Updated constructor to use `[FromKeyedServices("storyblok")]` attribute
- Resolves the "storyblok" keyed service instance

#### `src/Covalent.Silo/Program.cs`

- Replaced manual `AddStoryblok()` call with `builder.Services.AddHitch()`
- Plugins are now auto-discovered and attached via Hitch

#### `src/Covalent.Silo/Covalent.Silo.csproj`

- Added project reference to Hitch

#### `src/Covalent.AppHost/AppHost.cs`

- Demonstrates new API with Aspire parameters:

```csharp
var storyblokSpaceId = builder.AddParameter("storyblok-space-id", secret: true);
var storyblokApiUrl = builder.AddParameter("storyblok-api-url", secret: true);
var storyblokToken = builder.AddParameter("storyblok-token", secret: true);

var hitch = builder.AddHitch(hitchBuilder => hitchBuilder.WithFilePattern("Covalent.Plugins.*.dll"))
    .WithStoryblokManagement("storyblok", storyblokSpaceId, storyblokApiUrl, storyblokToken);

var silo = builder.AddProject<Projects.Covalent_Silo>("covalent-silo")
    .WithReference(hitch)  // This injects all Hitch environment variables
    .WithReference(chat)
    .WithReference(foundry);
```

#### `src/Covalent.AppHost/Covalent.AppHost.csproj`

- Added project reference to `Covalent.Aspire.Hosting.Storyblok`

### 5. Test Updates

#### Test Plugin Providers

- Updated `TestPluginProvider`, `CategorizedTestPluginProvider`, and `FailingPluginProvider` to implement new `IPluginProvider` signature
- All test providers now accept `IConfigurationSection` parameter

#### Test Cleanup

- Removed tests for `PublishAsConfiguration` method (since it was removed)
- All remaining 51 tests pass successfully

## Architecture

### Configuration Flow

1. **AppHost**: Defines parameters and calls `WithStoryblokManagement`
2. **Hitch Resource**: Stores configuration in `PluginConfigurations` dictionary with `ReferenceExpression` values
3. **WithReference**: When Silo calls `.WithReference(hitch)`, environment variables are generated via `GetEnvironmentExports`
4. **Environment Variables**: Injected into Silo as `Hitch__Plugins__Tool__Storyblok__{name}__{Property}`
5. **Silo Startup**: Calls `AddHitch()` which discovers plugins from assemblies
6. **Plugin Discovery**: Scans configuration sections under `Hitch:Plugins` to find category/subcategory/name hierarchy
7. **Plugin Provider**: Receives `IConfigurationSection` for its specific instance and registers keyed services
8. **Controller**: Resolves service using `[FromKeyedServices("storyblok")]`

### Configuration Path Pattern

**Environment Variables:**

```
Hitch__Plugins__{Category}__{SubCategory}__{name} = {name}
Hitch__Plugins__{Category}__{SubCategory}__{name}__{PropertyName} = {value}
```

**Configuration Sections:**

```
Hitch:Plugins:{Category}:{SubCategory}:{name}
```

**Example:**

```
HITCH__PLUGINS__TOOL__STORYBLOK__storyblok = "storyblok"
HITCH__PLUGINS__TOOL__STORYBLOK__storyblok__SpaceId = "12345"
HITCH__PLUGINS__TOOL__STORYBLOK__storyblok__ManagementApiUrl = "https://api.storyblok.com"
HITCH__PLUGINS__TOOL__STORYBLOK__storyblok__PersonalAccessToken = "secret-token"
```

### Configuration Format

The implementation supports two configuration formats for backward compatibility:

**New Format (Recommended)** - Service name is the configuration key:

```
Hitch:Plugins:Tool:Storyblok:storyblok = "storyblok"
Hitch:Plugins:Tool:Storyblok:storyblok:SpaceId = "12345"
```

**Old Format (Array-style)** - Service name is the configuration value:

```
Hitch:Plugins:Database:Postgres:0 = "Connection1"
Hitch:Plugins:Database:Postgres:1 = "Connection2"
```

The `HitchBuilder.ProcessCategorizedPlugin` method automatically detects which format is used:

- If a section has a non-empty value, use the value (old format)
- If a section's key is non-numeric, use the key (new format)

### Keyed Services

- Plugins with a name register as keyed services
- Plugins without a name use non-keyed registration (backward compatible)
- Each named instance is completely isolated with its own configuration and HttpClient
- Controllers use `[FromKeyedServices("name")]` to resolve specific instances

## Benefits

1. **Multiple Instances**: Support multiple instances of the same plugin type (e.g., multiple Storyblok spaces)
2. **Type Safety**: Aspire parameters provide compile-time checking and secure secret handling
3. **Isolation**: Each instance has its own configuration, options, and dependencies
4. **Fluent API**: Clean, chainable configuration in AppHost
5. **Backward Compatible**: Plugins without names still work as before; old configuration format still supported
6. **Extensible**: Pattern can be applied to any Hitch plugin
7. **No JSON Parsing**: Uses native .NET configuration system with individual environment variables
8. **Aspire Integration**: Full support for Aspire parameters, references, and dependency injection

## Key Architectural Decisions

1. **Removed PublishAsConfiguration**: The method didn't work correctly with Aspire's configuration system. Instead, `GetEnvironmentExports` is called directly by `WithReference`.

2. **Individual Environment Variables**: Instead of a single JSON blob, each configuration value gets its own environment variable. This works better with Aspire's configuration system and is more debuggable.

3. **Backward Compatible Configuration Format**: Supporting both array-style and key-based configuration ensures existing setups continue to work.

4. **Separate Aspire Hosting Project**: `Covalent.Aspire.Hosting.Storyblok` keeps plugin-specific Aspire extensions separate from the core Hitch framework, following good separation of concerns.

5. **Keyed Services**: Using .NET's built-in keyed dependency injection provides type-safe, isolated service instances without custom service locator patterns.

## Testing

All tests pass successfully:

- **Hitch.Tests**: 29 tests ✅
- **Hitch.Aspire.Hosting.Tests**: 22 tests ✅
- **Total**: 51 tests passing

Tests cover:

- Plugin discovery and attachment
- Multiple service names for same plugin type
- Configuration format compatibility (both old and new)
- Keyed service resolution
- Environment variable generation
- Error handling and edge cases
- Aspire resource builder functionality

## Build Status

✅ **Solution builds successfully with no errors**  
⚠️ 9 warnings (all pre-existing, unrelated to this implementation)  
✅ **All 51 tests passing**

## Usage Example

### AppHost Configuration

```csharp
using Aspire.Hosting.Azure;
using Hitch.Aspire.Hosting;

var builder = DistributedApplication.CreateBuilder(args);

// Define parameters for Storyblok
var storyblokSpaceId = builder.AddParameter("storyblok-space-id");
var storyblokApiUrl = builder.AddParameter("storyblok-api-url");
var storyblokToken = builder.AddParameter("storyblok-token", secret: true);

// Configure Hitch with Storyblok plugin
var hitch = builder.AddHitch(hitchBuilder =>
        hitchBuilder.WithFilePattern("Covalent.Plugins.*.dll"))
    .WithStoryblokManagement("storyblok", storyblokSpaceId, storyblokApiUrl, storyblokToken);

// Add Silo with Hitch reference
var silo = builder.AddProject<Projects.Covalent_Silo>("covalent-silo")
    .WithReference(hitch)  // Injects all Hitch configuration
    .WithExternalHttpEndpoints();

builder.Build().Run();
```

### Silo Configuration

```csharp
var builder = Host.CreateApplicationBuilder(args);

builder.AddServiceDefaults();
builder.AddCovalent();

// Hitch will auto-discover and attach plugins
builder.Services.AddHitch();
```

### Controller Usage

```csharp
[ApiController]
[Route("api/[controller]")]
public class StoryblokController : ControllerBase
{
    private readonly IStoryblokManagementService _storyblokManagementService;

    public StoryblokController(
        [FromKeyedServices("storyblok")] IStoryblokManagementService storyblokManagementService)
    {
        _storyblokManagementService = storyblokManagementService;
    }

    [HttpGet("components")]
    public async Task<IEnumerable<Component>> GetComponents()
    {
        return await _storyblokManagementService.GetComponents();
    }
}
```

## Future Enhancements

Potential future improvements:

1. Add support for plugin lifecycle hooks (initialization, shutdown)
2. Create more Aspire hosting extensions for other plugins
3. Add health checks for plugin instances
4. Create scaffolding tools to generate new plugins
5. Add plugin versioning and dependency management
