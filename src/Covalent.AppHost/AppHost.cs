var builder = DistributedApplication.CreateBuilder(args);

var foundry = builder.AddAzureFoundry("namedAzureFoundry");

builder.AddProject<Projects.Covalent_Silo>("covalent-silo")
    .WithReference(foundry);

builder.Build().Run();
