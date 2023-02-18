using CleanCRM.Application.Common.Exceptions;
using CleanCRM.Application.Common.Interfaces;
using CleanCRM.Application.Common.Models;
using CleanCRM.Application.Customers.Common;
using CleanCRM.Domain.Entities.Customers;
using MediatR;

namespace CleanCRM.Application.Customers.Queries.GetCustomer;

public record GetCustomerQuery : IRequest<ItemResult<CustomerDto>>, IApiRequest
{
    public int Id { get; set; }
}


public class GetCustomerQueryHandler : IRequestHandler<GetCustomerQuery, ItemResult<CustomerDto>>
{
    private readonly IApplicationDbContext _context;

    public GetCustomerQueryHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<ItemResult<CustomerDto>> Handle(GetCustomerQuery request, CancellationToken cancellationToken)
    {
        var entity = await _context.Customers.FindAsync(new object?[] { request.Id }, cancellationToken: cancellationToken);

        if (entity == null)
        {
            throw new NotFoundException(nameof(Customer), request.Id);
        }

        return new ItemResult<CustomerDto>()
        {
            Result = new CustomerDto(entity)
        };
    }
}