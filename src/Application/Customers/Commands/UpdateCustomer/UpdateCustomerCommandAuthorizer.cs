using CleanCRM.Application.Common.Authorization;

namespace CleanCRM.Application.Customers.Commands.UpdateCustomer;

public class UpdateCustomerCommandAuthorizer : AbstractRequestAuthorizer<UpdateCustomerCommand>
{
    public override void BuildPolicy(UpdateCustomerCommand request)
    {
        UseAllRequirement(new MustBeAuthorizedUserRequirement());
    }
}
