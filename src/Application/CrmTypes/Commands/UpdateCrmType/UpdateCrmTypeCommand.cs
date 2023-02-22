using CleanCRM.Application.Common.Exceptions;
using CleanCRM.Application.Common.Interfaces;
using CleanCRM.Application.Common.Models;
using CleanCRM.Application.CrmTypes.Common;
using CleanCRM.Domain.Entities.CrmTypes;
using CleanCRM.Domain.ValueObjects;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CleanCRM.Application.CrmTypes.Commands.UpdateCrmType;

public class UpdateCrmTypeCommand : IRequest<ItemResult<bool>>, IApiRequest
{
    public string Id { get; set; } = null!;

    public IDictionary<string, CrmTypeFieldPropertiesDto> Fields { get; set; } = new Dictionary<string, CrmTypeFieldPropertiesDto>();
}


public class UpdateCrmTypeCommandHandler : IRequestHandler<UpdateCrmTypeCommand, ItemResult<bool>>
{
    private readonly IApplicationDbContext _context;

    public UpdateCrmTypeCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<ItemResult<bool>> Handle(UpdateCrmTypeCommand request, CancellationToken cancellationToken)
    {
        var entity = await _context.CrmTypes.Include(x => x.Fields)
            .FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);

        if (entity == null)
        {
            throw new NotFoundException(nameof(CrmType), request.Id);
        }

        _context.CrmTypeFields.RemoveRange(entity.Fields);

        foreach (var fieldPair in request.Fields)
        {
            var fieldEntity = entity.Fields.FirstOrDefault(x => x.Name.Equals(fieldPair.Key, StringComparison.OrdinalIgnoreCase), new CrmTypeField()
            {
                Name = fieldPair.Key,
                FieldType = CrmFieldType.From(fieldPair.Value.Type)
            });

            if (!entity.Fields.Contains(fieldEntity))
            {
                entity.Fields.Add(fieldEntity);
            }
        }

        await _context.SaveChangesAsync(cancellationToken);

        return new ItemResult<bool>()
        {
            Result = true
        };
    }
}
