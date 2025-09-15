var builder = DistributedApplication.CreateBuilder(args);

var foundry = builder.AddAzureFoundry("namedAzureFoundry");

var silo =builder.AddProject<Projects.Covalent_Silo>("covalent-silo")
    .WithReference(foundry)
    .WithExternalHttpEndpoints();

silo.WithUrl("/api/providers", "Providers");

var app = builder.AddNpmApp("covalent-app", "../../covalent-app", "dev")
    .WithReference(silo)
    .WaitFor(silo)
    .WithEnvironment("BROWSER", "none") // Disable opening browser on npm start
    .WithHttpEndpoint(env: "PORT")
    .WithExternalHttpEndpoints();

builder.Build().Run();
