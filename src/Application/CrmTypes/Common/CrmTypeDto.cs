using CleanCRM.Application.CrmItems.Common;
using CleanCRM.Domain.Entities.CrmTypes;

namespace CleanCRM.Application.CrmTypes.Common;

public record CrmTypeDto
{
    public string Id { get; init; }
    public IDictionary<string, CrmTypeFieldDto> Fields { get; private init; } = new Dictionary<string, CrmTypeFieldDto>();
    public CrmTypeDto(CrmType entity)
    {
        Id = entity.Id;

        foreach (var item in entity.Fields)
        {
            Fields.Add(item.Name, new CrmTypeFieldDto(item));
        }
    }
}
