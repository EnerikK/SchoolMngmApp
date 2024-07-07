var builder = DistributedApplication.CreateBuilder(args);

builder.AddProject<Projects.SchoolApp_Web>("web");

builder.Build().Run();
