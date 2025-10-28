using Aspire.Hosting.Azure;
using Hitch.Aspire.Hosting;

var builder = DistributedApplication.CreateBuilder(args);

// configure our plugins!
var hitch = builder.AddHitch(builder => builder.WithFilePattern("Covalent.Plugins.*.dll"));

var foundry = builder.AddAzureAIFoundry("foundry");
var chat = foundry.AddDeployment("chat", AIFoundryModel.OpenAI.Gpt5Nano);

var temporal = builder.AddTemporalServerContainer("temporal");

var silo = builder.AddProject<Projects.Covalent_Silo>("covalent-silo")
    .WithReference(hitch)
    .WithReference(chat)
    .WithReference(foundry)
    .WithReference(temporal)
    .WithExternalHttpEndpoints();

silo.WithUrl("/api/providers", "Providers");

var app = builder.AddNpmApp("covalent-app", "../../covalent-app", "dev")
    .WithReference(silo)
    .WaitFor(silo)
    .WithEnvironment("BROWSER", "none") // Disable opening browser on npm start
    .WithHttpEndpoint(env: "PORT") // inform node of the PORT to listen on
    .WithExternalHttpEndpoints();

builder.Build().Run();
