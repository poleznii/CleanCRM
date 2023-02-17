using CleanCRM.Application.Common.Exceptions;
using CleanCRM.Application.Common.Interfaces;
using CleanCRM.Application.Common.Models;
using CleanCRM.Domain.Entities.Customers;
using MediatR;

namespace CleanCRM.Application.Customers.Commands.DeleteCustomer;

public record DeleteCustomerCommand : IRequest<Result>
{
    public int Id { get; set; }
}

public class DeleteCustomerCommandHandler : IRequestHandler<DeleteCustomerCommand, Result>
{
    private readonly IApplicationDbContext _context;

    public DeleteCustomerCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Result> Handle(DeleteCustomerCommand request, CancellationToken cancellationToken)
    {
        var entity = await _context.Customers.FindAsync(new object?[] { request.Id }, cancellationToken: cancellationToken);

        if (entity == null)
        {
            throw new NotFoundException(nameof(Customer), request.Id);
        }

        _context.Customers.Remove(entity);

        await _context.SaveChangesAsync(cancellationToken);
        return Result.Success();
    }
}
