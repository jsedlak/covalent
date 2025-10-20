using Covalent;
using Covalent.Providers;
using Microsoft.AspNetCore.Http.Features;

var builder = WebApplication.CreateBuilder(args);

builder.AddServiceDefaults();

// Add support for covalent!
builder.AddCovalent();
builder.AddAzureChatCompletionsClient(connectionName: "chat")
    .AddChatClient();

// Add ASP.NET Core services
builder.Services.AddCors();
builder.Services.AddControllers();
builder.Services.AddOpenApi();

var app = builder.Build();

app.MapDefaultEndpoints();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseCors(policy => policy
    .AllowAnyOrigin()
    .AllowAnyMethod()
    .AllowAnyHeader())
;

app.UseHttpsRedirection();
app.UseAuthorization();

app.Use(async (context, next) =>
{
    // Disable compression on this route
    if (context.Request.Path.StartsWithSegments("/chat2"))
    {
        context.Features.Get<IHttpResponseBodyFeature>()?.DisableBuffering();
    }

    await next();
});

app.MapControllers();

app.Run();
