using CleanCRM.Domain.Entities.CrmItems;

namespace CleanCRM.Application.CrmItems.Common;

public record CrmItemFieldDto
{
    public string Name { get; init; }
    public string Type { get; init; }
    public IEnumerable<string?> Values { get; init; }
    public CrmItemFieldDto(CrmItemField entity)
    {
        Name = entity.Field.Name;
        Type = entity.Field.FieldType;
        Values = entity.Values.Select(x => x.Raw);
    }
}
