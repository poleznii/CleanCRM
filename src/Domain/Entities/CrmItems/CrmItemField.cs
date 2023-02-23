using CleanCRM.Domain.Common;
using CleanCRM.Domain.Entities.CrmTypes;

namespace CleanCRM.Domain.Entities.CrmItems;

public class CrmItemField : BaseEntity<string>
{
    public string ItemId { get; set; }
    public CrmItem Item { get; set; }

    public string FieldId { get; set; }
    public CrmTypeField Field { get; set; }


    public IList<CrmItemPropertyValue> Values { get; private set; } = new List<CrmItemPropertyValue>();
}
