using CleanCRM.Application.Common.Exceptions;
using CleanCRM.Application.Common.Interfaces;
using CleanCRM.Application.Customers.Common;
using CleanCRM.Domain.Entities.Customers;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CleanCRM.Application.Customers.Queries.GetCustomer;

public record GetCustomerQuery : IRequest<CustomerDto>
{
    public int Id { get; set; }
}


public class GetCustomerQueryHandler : IRequestHandler<GetCustomerQuery, CustomerDto>
{
    private readonly IApplicationDbContext _context;

    public GetCustomerQueryHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<CustomerDto> Handle(GetCustomerQuery request, CancellationToken cancellationToken)
    {
        var entity = await _context.Customers.FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);

        if (entity == null)
        {
            throw new NotFoundException(nameof(Customer), request.Id);
        }

        return new CustomerDto(entity);
    }
}