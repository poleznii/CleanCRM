using CleanCRM.Domain.Entities.CrmTypes;
using CleanCRM.Domain.Entities.CrmItems;
using CleanCRM.Domain.Entities.Customers;
using Microsoft.EntityFrameworkCore;

namespace CleanCRM.Application.Common.Interfaces;

public interface IApplicationDbContext
{
    DbSet<Customer> Customers { get; }


    DbSet<CrmItem> CrmItems { get; }
    DbSet<CrmItemField> CrmItemProperties { get; }
    DbSet<CrmItemPropertyValue> CrmItemPropertyValues { get; }

    DbSet<CrmType> CrmTypes { get; }
    DbSet<CrmTypeField> CrmTypeFields { get; }

    Task<int> SaveChangesAsync(CancellationToken cancellationToken);
}
