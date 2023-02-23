using CleanCRM.Application.CrmTypes.Commands.CreateCrmType;
using CleanCRM.Application.CrmTypes.Common;
using CleanCRM.Domain.ValueObjects;

namespace CleanCRM.Application.IntegrationTests.CrmTypes.Common;

internal class CrmTypeTests
{
    /// <summary>
    /// Create Crm Type command
    /// </summary>
    /// <returns> CreateCrmTypeCommand with id = customer, and 3 fields: title, description, views</returns>
    public static CreateCrmTypeCommand GetCreateTypeCommand()
    {
        return new CreateCrmTypeCommand()
        {
            Id = "customer",
            Fields = new Dictionary<string, CrmTypeFieldPropertiesDto>()
            {
                { "title", new CrmTypeFieldPropertiesDto() { Type = CrmFieldType.String } },
                { "description", new CrmTypeFieldPropertiesDto() { Type = CrmFieldType.String } },
                { "views", new CrmTypeFieldPropertiesDto() { Type = CrmFieldType.Integer } }
            }
        };
    }
}
