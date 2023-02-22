using CleanCRM.Domain.Common;
using CleanCRM.Domain.Entities.CrmItems;

namespace CleanCRM.Domain.Entities.CrmTypes;

public class CrmType : BaseEntity<string>
{

    public IList<CrmItem> Items { get; private set; } = new List<CrmItem>();
    public IList<CrmTypeField> Fields { get; private set; } = new List<CrmTypeField>();
}
