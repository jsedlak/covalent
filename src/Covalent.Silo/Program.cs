using Covalent;
using Covalent.Providers;

var builder = WebApplication.CreateBuilder(args);

builder.AddServiceDefaults();

// Add support for covalent!
builder.AddCovalent();

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
app.MapControllers();

app.Run();
