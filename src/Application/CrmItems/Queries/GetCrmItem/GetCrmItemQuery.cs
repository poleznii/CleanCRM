using CleanCRM.Application.Common.Exceptions;
using CleanCRM.Application.Common.Interfaces;
using CleanCRM.Application.Common.Models;
using CleanCRM.Application.CrmItems.Common;
using CleanCRM.Domain.Entities.Customers;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CleanCRM.Application.CrmItems.Queries.GetCrmItem;

public class GetCrmItemQuery : IRequest<ItemResult<CrmItemDto>>, IApiRequest
{
    public string Id { get; init; } = null!;
    public string Type { get; set; } = null!;
}

public class GetCrmItemQueryHandler : IRequestHandler<GetCrmItemQuery, ItemResult<CrmItemDto>>
{
    private readonly IApplicationDbContext _context;

    public GetCrmItemQueryHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<ItemResult<CrmItemDto>> Handle(GetCrmItemQuery request, CancellationToken cancellationToken)
    {
        var entity = await _context.CrmItems
                                .Include(x => x.Type).ThenInclude(x => x.Fields)
                                .Include(x => x.Fields).ThenInclude(x => x.Values)
                                .FirstOrDefaultAsync(x => x.TypeId == request.Type && x.Id == request.Id, cancellationToken);

        if (entity == null)
        {
            throw new NotFoundException(nameof(Customer), request.Id);
        }

        return new ItemResult<CrmItemDto>() { Result = new CrmItemDto(entity) };
    }
}
