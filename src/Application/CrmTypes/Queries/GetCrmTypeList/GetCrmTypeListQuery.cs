using CleanCRM.Application.Common.Interfaces;
using CleanCRM.Application.Common.Models;
using CleanCRM.Application.CrmTypes.Common;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CleanCRM.Application.CrmTypes.Queries.GetCrmTypeList;

public class GetCrmTypeListQuery : IRequest<ListResult<CrmTypeDto>>, IApiRequest
{
    public int Skip { get; init; } = 0;
    public int Take { get; init; } = 10;
}

public class GetCrmTypeListQueryHandler : IRequestHandler<GetCrmTypeListQuery, ListResult<CrmTypeDto>>
{
    private readonly IApplicationDbContext _context;

    public GetCrmTypeListQueryHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<ListResult<CrmTypeDto>> Handle(GetCrmTypeListQuery request, CancellationToken cancellationToken)
    {
        var query = _context.CrmTypes.Include(x => x.Fields).OrderBy(x => x.Id).AsQueryable();

        var total = await query.CountAsync(cancellationToken);
        var entities = await query.Skip(request.Skip).Take(request.Take).ToListAsync(cancellationToken);

        return new ListResult<CrmTypeDto>(entities.Select(x => new CrmTypeDto(x)), total);
    }
}
