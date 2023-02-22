using CleanCRM.Domain.Common;

namespace CleanCRM.Domain.Entities.CrmItems;

public class CrmItemPropertyValue : BaseEntity<string>
{
    public string PropertyId { get; set; }
    public CrmItemField Property { get; set; }

    public string? Raw { get; set; }
}
