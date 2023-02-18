using CleanCRM.Application.Common.Interfaces;
using CleanCRM.Application.Common.Models;
using CleanCRM.Application.Customers.Common;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CleanCRM.Application.Customers.Queries.GetCustomerList;

public record GetCustomerListQuery : IRequest<ListResult<CustomerDto>>, IApiRequest
{
    public int Skip { get; init; } = 0;
    public int Take { get; init; } = 10;
}

public class GetCustomerListQueryHandler : IRequestHandler<GetCustomerListQuery, ListResult<CustomerDto>>
{
    private readonly IApplicationDbContext _context;

    public GetCustomerListQueryHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<ListResult<CustomerDto>> Handle(GetCustomerListQuery request, CancellationToken cancellationToken)
    {
        var query = _context.Customers.OrderBy(x => x.Id).AsQueryable();

        var total = await query.CountAsync(cancellationToken);
        var entities = await query.Skip(request.Skip).Take(request.Take).ToListAsync(cancellationToken);

        return new ListResult<CustomerDto>(entities.Select(x => new CustomerDto(x)), total);
    }
}