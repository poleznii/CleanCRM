using CleanCRM.Application.Common.Exceptions;
using CleanCRM.Application.Common.Interfaces;
using CleanCRM.Application.Common.Models;
using CleanCRM.Domain.Entities.Customers;
using CleanCRM.Domain.Events.Customers;
using MediatR;

namespace CleanCRM.Application.Customers.Commands.UpdateCustomer;

public record UpdateCustomerCommand : IRequest<ItemResult<bool>>, IApiRequest
{
    public int Id { get; set; }
    public string? Name { get; set; }
}

public class UpdateCustomerCommandHandler : IRequestHandler<UpdateCustomerCommand, ItemResult<bool>>
{
    private readonly IApplicationDbContext _context;

    public UpdateCustomerCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<ItemResult<bool>> Handle(UpdateCustomerCommand request, CancellationToken cancellationToken)
    {
        var entity = await _context.Customers.FindAsync(new object?[] { request.Id }, cancellationToken: cancellationToken);

        if (entity == null)
        {
            throw new NotFoundException(nameof(Customer), request.Id);
        }

        entity.Name = request.Name;

        entity.DomainEventAdd(new CustomerUpdatedEvent(entity));

        await _context.SaveChangesAsync(cancellationToken);

        return new ItemResult<bool>() { Result = true };
    }
}
