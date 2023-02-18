using CleanCRM.Infrastructure.Data;
using MediatR;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Respawn;
using Respawn.Graph;
using Microsoft.EntityFrameworkCore;
using CleanCRM.Infrastructure.Identity;
using Microsoft.AspNetCore.Identity;

namespace CleanCRM.Application.IntegrationTests;

[SetUpFixture]
public class Tests
{
    private static WebApplicationFactory<Program> _factory = null!;
    private static IServiceScopeFactory _scopeFactory = null!;
    private static IConfiguration _configuration = null!;
    private static string _connectionString = null!;
    private static Respawner _respawner = null!;

    private static string? _currentUserId;
    private static string? _currentUserName;

    [OneTimeSetUp]
    public static async Task BeforeAnyTests()
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

    public static async Task ResetState()
    {
        await _respawner.ResetAsync(_connectionString);

        _currentUserId = null;
        _currentUserName = null;
    }

    public static void RunAsUnauthorizedUser()
    {
        _currentUserId = null;
        _currentUserName = null;
    }

    public static async Task RunAsTestUser()
    {
        await RunAsUserAsync("test@localhost", "SomePassword1!");
    }

    public static async Task<string> RunAsUserAsync(string userName, string password)
    {
        using var scope = _scopeFactory.CreateScope();

        var userManager = scope.ServiceProvider.GetRequiredService<UserManager<ApplicationUser>>();

        var user = new ApplicationUser { UserName = userName, Email = userName };

        var result = await userManager.CreateAsync(user, password);


        if (!result.Succeeded)
        {
            var errors = string.Join(Environment.NewLine, result.Errors.Select(e => e.Description));
            throw new Exception($"Unable to create {userName}.{Environment.NewLine}{errors}");
        }

        _currentUserId = user.Id;
        _currentUserName = user.UserName;
        return _currentUserId;
    }

    public static async Task<TResponse> SendAsync<TResponse>(IRequest<TResponse> request)
    {
        using var scope = _scopeFactory.CreateScope();

        var mediator = scope.ServiceProvider.GetRequiredService<ISender>();

        return await mediator.Send(request);
    }

    public static async Task<TEntity?> FindAsync<TEntity>(params object[] keyValues) where TEntity : class
    {
        using var scope = _scopeFactory.CreateScope();

        var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

        return await context.FindAsync<TEntity>(keyValues);
    }

    public static string? GetCurrentUserId()
    {
        return _currentUserId;
    }

    public static string? GetCurrentUserName()
    {
        return _currentUserName;
    }
}
