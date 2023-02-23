using CleanCRM.Domain.Entities.CrmItems;

namespace CleanCRM.Application.CrmItems.Common;

public record CrmItemDto
{
    public string Id { get; init; }
    public string Type { get; init; }
    public IDictionary<string, CrmItemFieldDto> Fields { get; private init; } = new Dictionary<string, CrmItemFieldDto>();
    public CrmItemDto(CrmItem entity)
    {
        Id = entity.Id;
        Type = entity.TypeId;
        foreach (var item in entity.Fields)
        {
            Fields.Add(item.Field.Name, new CrmItemFieldDto(item));
        }
    }
}
