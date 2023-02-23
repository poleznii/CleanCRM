using CleanCRM.Domain.Entities.CrmTypes;
using System.Text.Json.Serialization;

namespace CleanCRM.Application.CrmItems.Common;

public record CrmTypeFieldDto
{
    public IDictionary<string, object> Metadata { get; private init; } = new Dictionary<string, object>();

    [JsonConstructor]
    public CrmTypeFieldDto() { }

    public CrmTypeFieldDto(CrmTypeField entity)
    {
        Metadata.Add("type", entity.FieldType.ToString());
    }
}
