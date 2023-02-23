using CleanCRM.Application.Common.Authorization;

namespace CleanCRM.Application.CrmTypes.Queries.GetCrmTypeList;

public class GetCrmTypeListQueryAuthorizer : AbstractRequestAuthorizer<GetCrmTypeListQuery>
{
    public override void BuildPolicy(GetCrmTypeListQuery request)
    {
        UseAllRequirement(new MustBeAuthorizedUserRequirement());
    }
}
