using MELC.Catalogo.API.Configuration;
using MELC.Main.API.Configuration;
using MELC.Main.API.Data;
using MELC.WebApi.Core.Identidade;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Configuration
    .SetBasePath(builder.Environment.ContentRootPath)
    .AddJsonFile("appsettings.json", true, true)
    .AddJsonFile($"appsettings.{builder.Environment.EnvironmentName}.json", true, true)
    .AddEnvironmentVariables();

builder.Services.AddSwaggerConfiguration();

if (builder.Environment.IsDevelopment())
    builder.Configuration.AddUserSecrets<Program>();

builder.Services.AddApiConfiguration(builder.Configuration);
builder.Services.AddJwtConfiguration(builder.Configuration);
builder.Services.RegisterServices();

var app = builder.Build();

using (var serviceScope = app.Services.CreateScope())
{
    var context = serviceScope.ServiceProvider.GetRequiredService<MelcContext>();
    context.Database.Migrate();
}

// Configure the HTTP request pipeline.
app.UseSwaggerConfiguration();
app.UseApiConfiguration(builder.Environment);

app.Run();