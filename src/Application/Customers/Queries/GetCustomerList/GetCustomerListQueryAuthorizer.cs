using CleanCRM.Application.Common.Authorization;

namespace CleanCRM.Application.Customers.Queries.GetCustomerList;

public class GetCustomerListQueryAuthorizer : AbstractRequestAuthorizer<GetCustomerListQuery>
{
    public override void BuildPolicy(GetCustomerListQuery request)
    {
        UseAllRequirement(new MustBeAuthorizedUserRequirement());
    }
}
