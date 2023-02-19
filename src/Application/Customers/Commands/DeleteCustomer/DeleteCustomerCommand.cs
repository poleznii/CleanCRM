using CleanCRM.Application.Common.Exceptions;
using CleanCRM.Application.Common.Interfaces;
using CleanCRM.Application.Common.Models;
using CleanCRM.Domain.Entities.Customers;
using CleanCRM.Domain.Events.Customers;
using MediatR;

namespace CleanCRM.Application.Customers.Commands.DeleteCustomer;

public record DeleteCustomerCommand : IRequest<ItemResult<bool>>, IApiRequest
{
    public int Id { get; set; }
}

public class DeleteCustomerCommandHandler : IRequestHandler<DeleteCustomerCommand, ItemResult<bool>>
{
    private readonly IApplicationDbContext _context;

    public DeleteCustomerCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<ItemResult<bool>> Handle(DeleteCustomerCommand request, CancellationToken cancellationToken)
    {
        var entity = await _context.Customers.FindAsync(new object?[] { request.Id }, cancellationToken: cancellationToken);

        if (entity == null)
        {
            throw new NotFoundException(nameof(Customer), request.Id);
        }

        _context.Customers.Remove(entity);

        entity.DomainEventAdd(new CustomerDeletedEvent(entity));

        await _context.SaveChangesAsync(cancellationToken);

        return new ItemResult<bool>() { Result = true };
    }
}
