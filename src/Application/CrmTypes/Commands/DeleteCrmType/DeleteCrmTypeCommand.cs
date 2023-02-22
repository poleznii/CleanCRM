using CleanCRM.Application.Common.Exceptions;
using CleanCRM.Application.Common.Interfaces;
using CleanCRM.Application.Common.Models;
using CleanCRM.Domain.Entities.CrmTypes;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CleanCRM.Application.CrmTypes.Commands.DeleteCrmType;

public class DeleteCrmTypeCommand : IRequest<ItemResult<bool>>, IApiRequest
{
    public string Id { get; set; } = null!;
}

public class DeleteCrmTypeCommandHandler : IRequestHandler<DeleteCrmTypeCommand, ItemResult<bool>>
{
    private readonly IApplicationDbContext _context;

    public DeleteCrmTypeCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<ItemResult<bool>> Handle(DeleteCrmTypeCommand request, CancellationToken cancellationToken)
    {
        var entity = await _context.CrmTypes.Include(x => x.Fields)
            .FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);

        if (entity == null)
        {
            throw new NotFoundException(nameof(CrmType), request.Id);
        }

        _context.CrmTypes.Remove(entity);

        await _context.SaveChangesAsync(cancellationToken);

        return new ItemResult<bool>()
        {
            Result = true
        };
    }
}
