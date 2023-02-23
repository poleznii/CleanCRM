using CleanCRM.Domain.Common;
using CleanCRM.Domain.Entities.CrmTypes;

namespace CleanCRM.Domain.Entities.CrmItems;

public class CrmItem : BaseEntity<string>
{
    public string TypeId { get; set; }
    public CrmType Type { get; set; }


    public IList<CrmItemField> Fields { get; private set; } = new List<CrmItemField>();
}
