using CleanCRM.Application.Common.Authorization;

namespace CleanCRM.Application.CrmItems.Queries.GetCrmItemList;

public class GetCrmItemListQueryAuthorizer : AbstractRequestAuthorizer<GetCrmItemListQuery>
{
    public override void BuildPolicy(GetCrmItemListQuery request)
    {
        UseAllRequirement(new MustBeAuthorizedUserRequirement());
    }
}
