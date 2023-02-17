var builder = WebApplication.CreateBuilder(args);

builder.Services.AddApiUIServices();

var app = builder.Build();

app.MapControllers();

app.MapGet("/", () => "Hello World!");

app.Run();
