using CleanCRM.Domain.Entities.CrmItems;

namespace CleanCRM.Application.CrmItems.Common;

public record CrmItemDto
{
    public string Id { get; init; }
    public string Type { get; init; }
    public IList<CrmItemFieldDto> Fields { get; private init; } = new List<CrmItemFieldDto>();
    public CrmItemDto(CrmItem entity)
    {
        Id = entity.Id;
        Type = entity.TypeId;
        foreach (var field in entity.Fields)
        {
            Fields.Add(new CrmItemFieldDto(field));
        }
    }
}
