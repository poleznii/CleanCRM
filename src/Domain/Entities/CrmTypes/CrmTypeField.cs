using CleanCRM.Domain.Common;
using CleanCRM.Domain.ValueObjects;

namespace CleanCRM.Domain.Entities.CrmTypes;

public class CrmTypeField : BaseEntity<string>
{
    public string CrmTypeId { get; set; }
    public CrmType CrmType { get; set; }

    public string Name { get; set; }
    public CrmFieldType FieldType { get; set; }

    //public IList<ContentTypeFieldParam> Params { get; private set; } = new List<ContentTypeFieldParam>();
}
