using CleanCRM.Application.Common.Exceptions;
using CleanCRM.Application.Common.Interfaces;
using CleanCRM.Domain.Entities.Customers;
using MediatR;

namespace CleanCRM.Application.Customers.Commands.EditCustomer;

public record UpdateCustomerCommand : IRequest
{
    public int Id { get; set; }
    public string? Name { get; set; }
}

public class UpdateCustomerCommandHandler : IRequestHandler<UpdateCustomerCommand>
{
    private readonly IApplicationDbContext _context;

    public UpdateCustomerCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task Handle(UpdateCustomerCommand request, CancellationToken cancellationToken)
    {
        var entity = await _context.Customers.FindAsync(new object?[] { request.Id }, cancellationToken: cancellationToken);

        if (entity == null)
        {
            throw new NotFoundException(nameof(Customer), request.Id);
        }

        entity.Name = request.Name;

        await _context.SaveChangesAsync(cancellationToken);
    }
}
