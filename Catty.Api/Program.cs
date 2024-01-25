var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddDefaultWebAppService();
builder.Services.AddExternalWebAppService();
builder.Services.AddInternalWebAppService(builder.Configuration);


var app = builder.Build();

// Configure the HTTP request pipeline.
app.UsePipelineMiddleWares();

app.Run();
