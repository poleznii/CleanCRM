using CleanCRM.Infrastructure.Data;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddApplicationServices();

var useInMemoryDatabase = builder.Configuration.GetValue<bool>("UseInMemoryDatabase");
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddInfrastructureServices(useInMemoryDatabase, connectionString);

builder.Services.AddApiUIServices();

var app = builder.Build();

// Seed database
using (var scope = app.Services.CreateScope())
{
    var initialiser = scope.ServiceProvider.GetRequiredService<ApplicationDbInitialiser>();
    await initialiser.InitAsync();
    await initialiser.SeedAsync();
}

app.MapControllers();

app.MapGet("/", () => "Hello World!");

app.Run();
