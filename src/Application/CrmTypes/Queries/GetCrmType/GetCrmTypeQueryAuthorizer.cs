using CleanCRM.Application.Common.Authorization;

namespace CleanCRM.Application.CrmTypes.Queries.GetCrmType;

public class GetCrmTypeQueryAuthorizer : AbstractRequestAuthorizer<GetCrmTypeQuery>
{
    public override void BuildPolicy(GetCrmTypeQuery request)
    {
        UseAllRequirement(new MustBeAuthorizedUserRequirement());
    }
}
