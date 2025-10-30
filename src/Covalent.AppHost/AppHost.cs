using Aspire.Hosting.Azure;
using Hitch.Aspire.Hosting;

var builder = DistributedApplication.CreateBuilder(args);

// Configure Storyblok parameters
var storyblokSpaceId = builder.AddParameter("storyblok-space-id", secret: true);
var storyblokApiUrl = builder.AddParameter("storyblok-api-url", secret: true);
var storyblokToken = builder.AddParameter("storyblok-token", secret: true);

// configure our plugins!
var hitch = builder.AddHitch(hitchBuilder => hitchBuilder.WithFilePattern("Covalent.Plugins.*.dll"))
    .WithStoryblokManagement("storyblok", storyblokSpaceId, storyblokApiUrl, storyblokToken);

var foundry = builder.AddAzureAIFoundry("foundry");
var chat = foundry.AddDeployment("chat", AIFoundryModel.OpenAI.Gpt5Nano);

// var temporal = builder.AddTemporalServerContainer("temporal");

var silo = builder.AddProject<Projects.Covalent_Silo>("covalent-silo")
    .WithReference(hitch)
    // .WithReference(chat)
    //.WithReference(foundry)
    //.WithReference(temporal)
    .WithExternalHttpEndpoints();

silo.WithUrl("/api/providers", "Providers");

var app = builder.AddNpmApp("covalent-app", "../../covalent-app", "dev")
    .WithReference(silo)
    .WaitFor(silo)
    .WithEnvironment("BROWSER", "none") // Disable opening browser on npm start
    .WithHttpEndpoint(env: "PORT") // inform node of the PORT to listen on
    .WithExternalHttpEndpoints();

builder.Build().Run();
