using CleanCRM.Application.Common.Exceptions;
using CleanCRM.Application.Common.Interfaces;
using CleanCRM.Application.Common.Models;
using CleanCRM.Domain.Entities.CrmItems;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CleanCRM.Application.CrmItems.Commands.DeleteCrmItem;

public class DeleteCrmItemCommand : IRequest<ItemResult<bool>>, IApiRequest
{
    public string Id { get; init; } = null!;
    public string Type { get; set; } = null!;
}

public class DeleteCrmItemCommandHandler : IRequestHandler<DeleteCrmItemCommand, ItemResult<bool>>
{
    private readonly IApplicationDbContext _context;

    public DeleteCrmItemCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<ItemResult<bool>> Handle(DeleteCrmItemCommand request, CancellationToken cancellationToken)
    {
        var entity = await _context.CrmItems.Include(x => x.Fields).ThenInclude(x => x.Values)
                                            .FirstOrDefaultAsync(x => x.Id == request.Id && x.TypeId == request.Type, cancellationToken);

        if (entity == null)
        {
            throw new NotFoundException(nameof(CrmItem), request.Id);
        }

        _context.CrmItems.Remove(entity);

        //entity.DomainEventAdd(new CustomerDeletedEvent(entity));

        await _context.SaveChangesAsync(cancellationToken);

        return new ItemResult<bool>() { Result = true };
    }
}