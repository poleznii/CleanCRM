using CleanCRM.Domain.Entities.Customers;
using Microsoft.EntityFrameworkCore;

namespace CleanCRM.Application.Common.Interfaces;

public interface IApplicationDbContext
{
    DbSet<Customer> Customers { get; }
    Task<int> SaveChangesAsync(CancellationToken cancellationToken);
}
