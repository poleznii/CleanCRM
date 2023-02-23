using CleanCRM.Domain.ValueObjects;
using System.Text.Json.Serialization;

namespace CleanCRM.Application.CrmTypes.Common;

public class CrmTypeFieldPropertiesDto
{
    public string Type { get; set; } = CrmFieldType.String;

    [JsonConstructor]
    public CrmTypeFieldPropertiesDto() { }
}
