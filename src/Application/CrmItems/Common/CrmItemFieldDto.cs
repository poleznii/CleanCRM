using CleanCRM.Domain.Entities.CrmItems;
using System.Text.Json.Serialization;

namespace CleanCRM.Application.CrmItems.Common;

public record CrmItemFieldDto
{
    public IList<string?> Values { get; init; } = new List<string?>();

    public IDictionary<string, object> Metadata { get; private init; } = new Dictionary<string, object>();

    [JsonConstructor]
    public CrmItemFieldDto() { }

    public CrmItemFieldDto(CrmItemField entity)
    {
        Values = entity.Values.Select(x => x.Raw).ToList();

        Metadata.Add("type", entity.Field.FieldType.ToString());
    }
}
