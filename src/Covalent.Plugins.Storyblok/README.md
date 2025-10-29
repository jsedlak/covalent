# Covalent.Plugins.Storyblok

Integration plugin for Storyblok CMS Management API.

## Installation

Add the package reference to your project:

```xml
<PackageReference Include="Covalent.Plugins.Storyblok" />
```

## Configuration

Add the following configuration to your `appsettings.json`:

```json
{
  "Storyblok": {
    "SpaceId": "your-space-id",
    "ManagementApiUrl": "https://mapi.storyblok.com/v1",
    "PersonalAccessToken": "your-personal-access-token"
  }
}
```

### Configuration Properties

- **SpaceId**: Your Storyblok space identifier
- **ManagementApiUrl**: The base URL for the Storyblok Management API (typically `https://mapi.storyblok.com/v1`)
- **PersonalAccessToken**: Your personal access token for authentication with the Management API

## Usage

### With ASP.NET Host Builder

```csharp
var builder = WebApplication.CreateBuilder(args);

// Add Storyblok services using default "Storyblok" configuration section
builder.AddStoryblok();

// Or specify a custom configuration section name
builder.AddStoryblok("CustomStoryblokSection");

// Or pass a configuration section directly
builder.AddStoryblok(builder.Configuration.GetSection("Storyblok"));

var app = builder.Build();
```

### With Service Collection

```csharp
services.AddStoryblok(configuration);

// Or with custom section name
services.AddStoryblok(configuration, "CustomSection");

// Or with configuration section
services.AddStoryblok(configuration.GetSection("Storyblok"));
```

### Using the Service

```csharp
public class MyService
{
    private readonly IStoryblokManagementService _storyblok;

    public MyService(IStoryblokManagementService storyblok)
    {
        _storyblok = storyblok;
    }

    public async Task<IEnumerable<Component>> GetAllComponents()
    {
        var spaceId = "12345"; // Your space ID
        return await _storyblok.GetComponents(spaceId);
    }
}
```

## Features

- **Configured HttpClient**: Automatically configures a keyed HttpClient with proper authorization headers
- **Dependency Injection**: Full support for ASP.NET Core dependency injection
- **Flexible Configuration**: Multiple ways to configure the plugin based on your needs
- **Scoped Services**: Management service is registered as scoped for proper lifecycle management
