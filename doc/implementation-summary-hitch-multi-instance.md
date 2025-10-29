# Hitch Multi-Instance Plugin Support - Implementation Summary

## Overview

Successfully implemented support for multiple named instances of Hitch plugins with keyed services, enabling configuration like:

```csharp
var hitch = builder.AddHitch(hitchBuilder => hitchBuilder.WithFilePattern("Covalent.Plugins.*.dll"))
    .WithStoryblokManagement("storyblok", spaceId, apiUrl, token)
    .WithStoryblokManagement("space2", spaceId2, apiUrl2, token2);
```

## Changes Made

### 1. Core Hitch Changes

#### `hitch/src/Hitch/IPluginProvider.cs`

- Updated `Attach` method signature to accept `IConfigurationSection` parameter
- Plugins now receive their configuration section directly

#### `hitch/src/Hitch/HitchBuilder.cs`

- Modified `ProcessCategorizedPlugin` to iterate through named instance sections
- Updated `AttachPlugin` to construct configuration section path: `Hitch:Plugins:{Category}:{SubCategory}:{name}`
- Plugins are now discovered by scanning child sections under each category/subcategory

#### `hitch/src/Hitch.Aspire.Hosting/HitchResource.cs`

- Added `PluginConfigurations` dictionary to store plugin instance configurations
- Stores configuration values keyed by `Category__SubCategory__ServiceName`

#### `hitch/src/Hitch.Aspire.Hosting/HitchResourceBuilderExtensions.cs`

- Updated `PublishAsConfiguration` to generate environment variables from `PluginConfigurations`
- Handles Aspire `ReferenceExpression` for parameter resolution
- Environment variables follow pattern: `Hitch__Plugins__{Category}__{SubCategory}__{name}__{PropertyName}`

### 2. New Aspire Hosting Project

#### `src/Covalent.Aspire.Hosting.Storyblok/`

- Created new project for Storyblok-specific Aspire hosting extensions
- Added to solution in "2. Aspire Injection" folder

#### `src/Covalent.Aspire.Hosting.Storyblok/HitchResourceBuilderExtensions.cs`

- Implemented `WithStoryblokManagement` extension method
- Accepts Aspire parameter resources for SpaceId, ManagementApiUrl, and PersonalAccessToken
- Registers plugin with Hitch and stores configuration in `PluginConfigurations`

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
var storyblokSpaceId = builder.AddParameter("storyblok-space-id");
var storyblokApiUrl = builder.AddParameter("storyblok-api-url");
var storyblokToken = builder.AddParameter("storyblok-token", secret: true);

var hitch = builder.AddHitch(hitchBuilder => hitchBuilder.WithFilePattern("Covalent.Plugins.*.dll"))
    .WithStoryblokManagement("storyblok", storyblokSpaceId, storyblokApiUrl, storyblokToken);
```

#### `src/Covalent.AppHost/Covalent.AppHost.csproj`

- Added project reference to `Covalent.Aspire.Hosting.Storyblok`

## Architecture

### Configuration Flow

1. **AppHost**: Defines parameters and calls `WithStoryblokManagement`
2. **Hitch Resource**: Stores configuration in `PluginConfigurations` dictionary
3. **Environment Variables**: Generated as `Hitch__Plugins__Tool__Storyblok__{name}__{Property}`
4. **Silo**: Calls `AddHitch()` which discovers plugins and passes configuration sections
5. **Plugin Provider**: Receives `IConfigurationSection` and registers keyed services
6. **Controller**: Resolves service using `[FromKeyedServices("storyblok")]`

### Configuration Path Pattern

```
Hitch__Plugins__{Category}__{SubCategory}__{name}__{PropertyName}

Example:
Hitch__Plugins__Tool__Storyblok__storyblok__SpaceId
Hitch__Plugins__Tool__Storyblok__storyblok__ManagementApiUrl
Hitch__Plugins__Tool__Storyblok__storyblok__PersonalAccessToken
```

### Keyed Services

- Plugins with a name register as keyed services
- Plugins without a name use non-keyed registration (backward compatible)
- Each named instance is completely isolated with its own configuration and HttpClient

## Benefits

1. **Multiple Instances**: Support multiple instances of the same plugin type
2. **Type Safety**: Aspire parameters provide compile-time checking
3. **Isolation**: Each instance has its own configuration and dependencies
4. **Fluent API**: Clean, chainable configuration in AppHost
5. **Backward Compatible**: Plugins without names still work as before
6. **Extensible**: Pattern can be applied to any Hitch plugin

## Build Status

✅ Solution builds successfully with no errors
⚠️ 9 warnings (all pre-existing, unrelated to this implementation)
