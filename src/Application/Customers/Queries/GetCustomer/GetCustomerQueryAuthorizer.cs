using CleanCRM.Application.Common.Authorization;

namespace CleanCRM.Application.Customers.Queries.GetCustomer;

public class GetCustomerQueryAuthorizer : AbstractRequestAuthorizer<GetCustomerQuery>
{
    public override void BuildPolicy(GetCustomerQuery request)
    {
        UseAllRequirement(new MustBeAuthorizedUserRequirement());
    }
}
