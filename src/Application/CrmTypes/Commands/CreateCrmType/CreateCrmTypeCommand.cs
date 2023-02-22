using CleanCRM.Application.Common.Interfaces;
using CleanCRM.Application.Common.Models;
using CleanCRM.Application.CrmTypes.Common;
using CleanCRM.Domain.Entities.CrmTypes;
using CleanCRM.Domain.ValueObjects;
using MediatR;

namespace CleanCRM.Application.CrmTypes.Commands.CreateCrmType;

public class CreateCrmTypeCommand : IRequest<ItemResult<string>>, IApiRequest
{
    public string Id { get; set; } = null!;

    public IDictionary<string, CrmTypeFieldPropertiesDto> Fields { get; set; } = new Dictionary<string, CrmTypeFieldPropertiesDto>();
}


public class CreateCrmTypeCommandHandler : IRequestHandler<CreateCrmTypeCommand, ItemResult<string>>
{
    private readonly IApplicationDbContext _context;

    public CreateCrmTypeCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<ItemResult<string>> Handle(CreateCrmTypeCommand request, CancellationToken cancellationToken)
    {
        var entity = new CrmType()
        {
            Id = request.Id.ToLowerInvariant(),
        };

        _context.CrmTypes.Add(entity);

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

        return new ItemResult<string>()
        {
            Result = entity.Id
        };
    }
}
