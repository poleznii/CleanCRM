using CleanCRM.Application.Common.Interfaces;
using CleanCRM.Infrastructure.Data;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Moq;

namespace CleanCRM.Application.IntegrationTests;

using static Tests;

internal class CustomWebApplicationFactory : WebApplicationFactory<Program>
{
    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.ConfigureAppConfiguration(configurationBuilder =>
        {
            var integrationConfig = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json")
                .AddEnvironmentVariables()
                .Build();

            configurationBuilder.AddConfiguration(integrationConfig);
        });

        builder.ConfigureServices((builder, services) =>
        {
            var descriptorUserService = services.SingleOrDefault(x => x.ServiceType == typeof(ICurrentUserService));
            if (descriptorUserService != null)
            {
                services.Remove(descriptorUserService);
            }
            services.AddTransient(provider => Mock.Of<ICurrentUserService>(x => x.UserId == GetCurrentUserId() && x.UserName == GetCurrentUserName()));


            var descriptorDb = services.SingleOrDefault(d => d.ServiceType == typeof(DbContextOptions<ApplicationDbContext>));
            if (descriptorDb != null)
            {
                services.Remove(descriptorDb);
            }

            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("TestConnection")));
        });
    }
}
