using Aspire.Hosting.Azure;

var builder = DistributedApplication.CreateBuilder(args);

var foundry = builder.AddAzureAIFoundry("foundry");
var chat = foundry.AddDeployment("chat", AIFoundryModel.OpenAI.Gpt5Nano);

var silo = builder.AddProject<Projects.Covalent_Silo>("covalent-silo")
    .WithReference(chat)
    .WithReference(foundry)
    .WithExternalHttpEndpoints();

silo.WithUrl("/api/providers", "Providers");

var app = builder.AddNpmApp("covalent-app", "../../covalent-app", "dev")
    .WithReference(silo)
    .WaitFor(silo)
    .WithEnvironment("BROWSER", "none") // Disable opening browser on npm start
    .WithHttpEndpoint(env: "PORT") // inform node of the PORT to listen on
    .WithExternalHttpEndpoints();

builder.Build().Run();
