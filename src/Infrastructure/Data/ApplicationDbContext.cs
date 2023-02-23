using CleanCRM.Application.Common.Interfaces;
using CleanCRM.Domain.Entities.CrmTypes;
using CleanCRM.Domain.Entities.CrmItems;
using CleanCRM.Domain.Entities.Customers;
using CleanCRM.Infrastructure.Identity;
using MediatR;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace CleanCRM.Infrastructure.Data;

public class ApplicationDbContext : IdentityDbContext<ApplicationUser>, IApplicationDbContext
{
    private readonly IMediator _mediator;

    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options, IMediator mediator) : base(options)
    {
        _mediator = mediator;
    }

    public DbSet<Customer> Customers => Set<Customer>();


    public DbSet<CrmItem> CrmItems => Set<CrmItem>();
    public DbSet<CrmItemField> CrmItemProperties => Set<CrmItemField>();
    public DbSet<CrmItemPropertyValue> CrmItemPropertyValues => Set<CrmItemPropertyValue>();

    public DbSet<CrmType> CrmTypes => Set<CrmType>();
    public DbSet<CrmTypeField> CrmTypeFields => Set<CrmTypeField>();


    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

        base.OnModelCreating(builder);
    }

    public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        await _mediator.PublishDomainEvents(this);

        return await base.SaveChangesAsync(cancellationToken);
    }
}
