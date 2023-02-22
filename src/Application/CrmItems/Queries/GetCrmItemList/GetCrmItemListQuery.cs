using CleanCRM.Application.Common.Interfaces;
using CleanCRM.Application.Common.Models;
using CleanCRM.Application.CrmItems.Common;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CleanCRM.Application.CrmItems.Queries.GetCrmItemList;

public class GetCrmItemListQuery : IRequest<ListResult<CrmItemDto>>, IApiRequest
{
    public string Type { get; set; }
    public int Skip { get; init; } = 0;
    public int Take { get; init; } = 10;
}

public class GetCrmItemListQueryHandler : IRequestHandler<GetCrmItemListQuery, ListResult<CrmItemDto>>
{
    private readonly IApplicationDbContext _context;

    public GetCrmItemListQueryHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<ListResult<CrmItemDto>> Handle(GetCrmItemListQuery request, CancellationToken cancellationToken)
    {
        var fields = _context.CrmTypeFields.Where(x => x.CrmTypeId.Equals(request.Type)).ToListAsync(cancellationToken);
        var query = _context.CrmItems.Include(x => x.Fields).ThenInclude(x => x.Values).Where(x => x.TypeId.Equals(request.Type)).OrderBy(x => x.Id).AsQueryable();

        var total = await query.CountAsync(cancellationToken);
        var entities = await query.Skip(request.Skip).Take(request.Take).ToListAsync(cancellationToken);

        return new ListResult<CrmItemDto>(entities.Select(x => new CrmItemDto(x)), total);
    }
}