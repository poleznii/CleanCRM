using CleanCRM.Infrastructure.Data;
using MediatR;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Respawn;
using Respawn.Graph;

namespace CleanCRM.Application.IntegrationTests;

public abstract class BaseTestFixture
{
    private WebApplicationFactory<Program> _factory = null!;
    private IServiceScopeFactory _scopeFactory = null!;
    private IConfiguration _configuration = null!;
    private string _connectionString = null!;
    private Respawner _respawner = null!;

    [OneTimeSetUp]
    public async Task BeforeAnyTests()
    {
        _factory = new CustomWebApplicationFactory();
        _scopeFactory = _factory.Services.GetRequiredService<IServiceScopeFactory>();
        _configuration = _factory.Services.GetRequiredService<IConfiguration>();
        _connectionString = _configuration.GetConnectionString("TestConnection") ?? string.Empty;

        _respawner = await Respawner.CreateAsync(_connectionString, new RespawnerOptions()
        {
            TablesToIgnore = new Table[]
            {
                "__EFMigrationsHistory"
            },
        });
    }

    [SetUp]
    public async Task TestSetUp()
    {
        await _respawner.ResetAsync(_connectionString);
    }

    protected async Task<TResponse> SendAsync<TResponse>(IRequest<TResponse> request)
    {
        using var scope = _scopeFactory.CreateScope();

        var mediator = scope.ServiceProvider.GetRequiredService<ISender>();

        return await mediator.Send(request);
    }

    protected async Task<TEntity?> FindAsync<TEntity>(params object[] keyValues) where TEntity : class
    {
        using var scope = _scopeFactory.CreateScope();

        var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

        return await context.FindAsync<TEntity>(keyValues);
    }
}