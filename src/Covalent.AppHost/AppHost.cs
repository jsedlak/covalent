var builder = DistributedApplication.CreateBuilder(args);

var foundry = builder.AddAzureFoundry("namedAzureFoundry");

var silo =builder.AddProject<Projects.Covalent_Silo>("covalent-silo")
    .WithReference(foundry);

silo.WithUrl("/api/providers", "Providers");

builder.Build().Run();
