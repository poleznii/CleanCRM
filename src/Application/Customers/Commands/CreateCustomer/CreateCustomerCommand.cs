using CleanCRM.Application.Common.Interfaces;
using CleanCRM.Application.Common.Models;
using CleanCRM.Domain.Entities.Customers;
using CleanCRM.Domain.Events.Customers;
using MediatR;

namespace CleanCRM.Application.Customers.Commands.CreateCustomer;

public record CreateCustomerCommand : IRequest<ItemResult<int>>, IApiRequest
{
    public string? Name { get; set; }
}

public class CreateCustomerCommandHandler : IRequestHandler<CreateCustomerCommand, ItemResult<int>>
{
    private readonly IApplicationDbContext _context;

    public CreateCustomerCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<ItemResult<int>> Handle(CreateCustomerCommand request, CancellationToken cancellationToken)
    {
        var entity = new Customer()
        {
            Name = request.Name,
        };

        _context.Customers.Add(entity);

        entity.DomainEventAdd(new CustomerCreatedEvent(entity));

        await _context.SaveChangesAsync(cancellationToken);

        return new ItemResult<int>()
        {
            Result = entity.Id
        };
    }
}
