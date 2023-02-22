using CleanCRM.Domain.Entities.CrmItems;
using System.Text.Json.Serialization;

namespace CleanCRM.Application.CrmItems.Common;

public record CrmItemFieldDto
{
    public IEnumerable<string?> Values { get; init; }

    public IDictionary<string, object> Metadata { get; private init; } = new Dictionary<string, object>();

    [JsonConstructor]
    public CrmItemFieldDto() { }

    public CrmItemFieldDto(CrmItemField entity)
    {
        Values = entity.Values.Select(x => x.Raw);

        Metadata.Add("type", entity.Field.FieldType.ToString());
    }
}
