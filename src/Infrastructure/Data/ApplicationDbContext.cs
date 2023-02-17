using CleanCRM.Application.Common.Interfaces;
using CleanCRM.Domain.Entities.Customers;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CleanCRM.Infrastructure.Data;

public class ApplicationDbContext : DbContext, IApplicationDbContext
{
    //TODO domain events
    private readonly IMediator _mediator;

    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options, IMediator mediator) : base(options)
    {
        _mediator = mediator;
    }

    public DbSet<Customer> Customers => Set<Customer>();
}
