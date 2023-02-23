using CleanCRM.Application.Common.Exceptions;
using CleanCRM.Application.Common.Interfaces;
using CleanCRM.Application.Common.Models;
using CleanCRM.Application.CrmTypes.Common;
using CleanCRM.Domain.Entities.CrmTypes;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CleanCRM.Application.CrmTypes.Queries.GetCrmType;

public class GetCrmTypeQuery : IRequest<ItemResult<CrmTypeDto>>, IApiRequest
{
    public string Id { get; init; } = null!;
}

public class GetCrmTypeQueryHandler : IRequestHandler<GetCrmTypeQuery, ItemResult<CrmTypeDto>>
{
    private readonly IApplicationDbContext _context;

    public GetCrmTypeQueryHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<ItemResult<CrmTypeDto>> Handle(GetCrmTypeQuery request, CancellationToken cancellationToken)
    {
        var entity = await _context.CrmTypes.Include(x => x.Fields)
                                .FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);

        if (entity == null)
        {
            throw new NotFoundException(nameof(CrmType), request.Id);
        }

        return new ItemResult<CrmTypeDto>() { Result = new CrmTypeDto(entity) };
    }
}
