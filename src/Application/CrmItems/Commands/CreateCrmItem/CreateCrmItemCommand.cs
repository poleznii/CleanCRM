using CleanCRM.Application.Common.Interfaces;
using CleanCRM.Application.Common.Models;
using CleanCRM.Application.CrmItems.Common;
using CleanCRM.Domain.Entities.CrmItems;
using MediatR;
using Microsoft.EntityFrameworkCore;


namespace CleanCRM.Application.CrmItems.Commands.CreateCrmItem;

public class CreateCrmItemCommand : IRequest<ItemResult<string>>, IApiRequest
{
    public string Type { get; set; } = null!;
    public IDictionary<string, CrmItemFieldDto> Fields { get; init; } = new Dictionary<string, CrmItemFieldDto>();
}

public class CreateCrmItemCommandHandler : IRequestHandler<CreateCrmItemCommand, ItemResult<string>>
{
    private readonly IApplicationDbContext _context;

    public CreateCrmItemCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<ItemResult<string>> Handle(CreateCrmItemCommand request, CancellationToken cancellationToken)
    {
        var typeEntity = await _context.CrmTypes.Include(x => x.Fields)
                                                .FirstAsync(x => x.Id == request.Type, cancellationToken);

        var entity = new CrmItem()
        {
            TypeId = request.Type
        };

        _context.CrmItems.Add(entity);

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

        //entity.DomainEventAdd(new CustomerCreatedEvent(entity));

        await _context.SaveChangesAsync(cancellationToken);

        return new ItemResult<string>()
        {
            Result = entity.Id
        };
    }
}
