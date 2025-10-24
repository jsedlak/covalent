var builder = DistributedApplication.CreateBuilder(args);

var foundry = builder.AddAzureFoundry("namedAzureFoundry");
var ollama = builder.AddOllama("ollama")
    .WithOpenWebUI()
    .WithDataVolume();
var model = ollama.AddModel("gemma3:1b");

var silo = builder.AddProject<Projects.Covalent_Silo>("covalent-silo")
    .WithReference(foundry)
    .WithReference(ollama)
    .WithExternalHttpEndpoints();

silo.WithUrl("/api/providers", "Providers");

var app = builder.AddNpmApp("covalent-app", "../../covalent-app", "dev")
    .WithReference(silo)
    .WaitFor(silo)
    .WithEnvironment("BROWSER", "none") // Disable opening browser on npm start
    .WithHttpEndpoint(env: "PORT") // inform node of the PORT to listen on
    .WithExternalHttpEndpoints();

builder.Build().Run();
