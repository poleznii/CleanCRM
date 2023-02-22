using CleanCRM.Application.Common.Exceptions;
using CleanCRM.Application.Common.Interfaces;
using CleanCRM.Application.Common.Models;
using CleanCRM.Application.CrmItems.Common;
using CleanCRM.Domain.Entities.CrmItems;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CleanCRM.Application.CrmItems.Commands.UpdateCrmItem;

public class UpdateCrmItemCommand : IRequest<ItemResult<bool>>, IApiRequest
{
    public string Id { get; init; } = null!;
    public string Type { get; set; } = null!;
    public IDictionary<string, CrmItemFieldDto> Fields { get; init; } = new Dictionary<string, CrmItemFieldDto>();
}

public class UpdateCrmItemCommandHandler : IRequestHandler<UpdateCrmItemCommand, ItemResult<bool>>
{
    private readonly IApplicationDbContext _context;

    public UpdateCrmItemCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<ItemResult<bool>> Handle(UpdateCrmItemCommand request, CancellationToken cancellationToken)
    {
        var entity = await _context.CrmItems.Include(x => x.Fields).ThenInclude(x => x.Values)
                                            .FirstOrDefaultAsync(x => x.Id == request.Id && x.TypeId == request.Type, cancellationToken);

        if (entity == null)
        {
            throw new NotFoundException(nameof(CrmItem), request.Id);
        }

        var typeEntity = await _context.CrmTypes.Include(x => x.Fields)
                                                .FirstAsync(x => x.Id == request.Type, cancellationToken);

        foreach (var fieldPair in request.Fields)
        {
            var fieldTypeEntity = typeEntity.Fields.FirstOrDefault(x => x.Name.Equals(fieldPair.Key, StringComparison.OrdinalIgnoreCase));
            if (fieldTypeEntity == null)
            {
                continue;
            }
            var fieldDto = fieldPair.Value;

            var fieldEntity = entity.Fields.FirstOrDefault(x => x.FieldId == fieldTypeEntity.Id, new CrmItemField()
            {
                Field = fieldTypeEntity
            });

            if (!entity.Fields.Contains(fieldEntity))
            {
                entity.Fields.Add(fieldEntity);
            }

            _context.CrmItemPropertyValues.RemoveRange(fieldEntity.Values);

            if (fieldDto.Values != null)
            {
                foreach (var value in fieldDto.Values)
                {
                    fieldEntity.Values.Add(new CrmItemPropertyValue()
                    {
                        Raw = value
                    });
                }
            }
        }

        await _context.SaveChangesAsync(cancellationToken);

        return new ItemResult<bool>()
        {
            Result = true
        };
    }
}