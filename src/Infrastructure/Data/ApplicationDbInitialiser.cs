using CleanCRM.Infrastructure.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace CleanCRM.Infrastructure.Data;

public class ApplicationDbInitialiser
{
    private readonly ILogger<ApplicationDbInitialiser> _logger;
    private readonly ApplicationDbContext _context;
    private readonly UserManager<ApplicationUser> _userManager;

    public ApplicationDbInitialiser(ILogger<ApplicationDbInitialiser> logger, ApplicationDbContext context, UserManager<ApplicationUser> userManager)
    {
        _logger = logger;
        _context = context;
        _userManager = userManager;
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
        if (!_userManager.Users.Any())
        {
            var admin = new ApplicationUser()
            {
                UserName = "admin@localhost",
                Email = "admin@localhost"
            };
            await _userManager.CreateAsync(admin, "Admin1!");
        }

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
