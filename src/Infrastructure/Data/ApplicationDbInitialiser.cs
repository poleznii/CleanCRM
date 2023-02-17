using CleanCRM.Domain.Entities.Customers;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace CleanCRM.Infrastructure.Data;

public class ApplicationDbInitialiser
{
    private readonly ILogger<ApplicationDbInitialiser> _logger;
    private readonly ApplicationDbContext _context;

    public ApplicationDbInitialiser(ILogger<ApplicationDbInitialiser> logger, ApplicationDbContext context)
    {
        _logger = logger;
        _context = context;
    }

    public async Task InitAsync()
    {
        try
        {
            if (_context.Database.IsSqlServer())
            {
                await _context.Database.MigrateAsync();
            }
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Database init error");
            throw;
        }
    }

    public async Task SeedAsync()
    {
        try
        {
            await TrySeedAsync();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Database seed error");
            throw;
        }
    }

    private async Task TrySeedAsync()
    {
        //if (!_context.Customers.Any())
        //{
        //    _context.Customers.AddRange(new[]
        //    {
        //        new Customer
        //        {
        //            Name = "Customer 1",
        //        },
        //        new Customer
        //        {
        //            Name = "Customer 2",
        //        },
        //        new Customer
        //        {
        //            Name = "Customer 3",
        //        }
        //    });

        //    await _context.SaveChangesAsync();
        //}
    }
}
