using CleanCRM.Application.Common.Interfaces;
using CleanCRM.Domain.Entities.Customers;
using MediatR;

namespace CleanCRM.Application.Customers.Commands.CreateCustomer;

public record CreateCustomerCommand : IRequest<int>
{
    public string? Name { get; set; }
}

public class CreateCustomerCommandHandler : IRequestHandler<CreateCustomerCommand, int>
{
    private readonly IApplicationDbContext _context;

    public CreateCustomerCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<int> Handle(CreateCustomerCommand request, CancellationToken cancellationToken)
    {
        var entity = new Customer()
        {
            Name = request.Name,
        };

        _context.Customers.Add(entity);

        await _context.SaveChangesAsync(cancellationToken);

        return entity.Id;
    }
}
