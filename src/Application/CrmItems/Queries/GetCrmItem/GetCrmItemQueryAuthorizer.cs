using CleanCRM.Application.Common.Authorization;

namespace CleanCRM.Application.CrmItems.Queries.GetCrmItem;

public class GetCrmItemQueryAuthorizer : AbstractRequestAuthorizer<GetCrmItemQuery>
{
    public override void BuildPolicy(GetCrmItemQuery request)
    {
        UseAllRequirement(new MustBeAuthorizedUserRequirement());
    }
}
