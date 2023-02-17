using CleanCRM.Infrastructure.Data;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddApplicationServices();
builder.Services.AddInfrastructureServices();
builder.Services.AddApiUIServices();

var app = builder.Build();

// Seed database
using (var scope = app.Services.CreateScope())
{
    var initialiser = scope.ServiceProvider.GetRequiredService<ApplicationDbInitialiser>();
    await initialiser.SeedAsync();
}

app.MapControllers();

app.MapGet("/", () => "Hello World!");

app.Run();
