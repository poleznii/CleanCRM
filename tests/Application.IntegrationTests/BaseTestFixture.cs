using CleanCRM.Infrastructure.Data;
using MediatR;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;

namespace CleanCRM.Application.IntegrationTests;

public abstract class BaseTestFixture
{
    private WebApplicationFactory<Program> _factory = null!;
    private IServiceScopeFactory _scopeFactory = null!;

    [OneTimeSetUp]
    public void BeforeAnyTests()
    {
        _factory = new CustomWebApplicationFactory();
        _scopeFactory = _factory.Services.GetRequiredService<IServiceScopeFactory>();
    }

    [OneTimeTearDown]
    public void AfterAnyTests()
    {

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