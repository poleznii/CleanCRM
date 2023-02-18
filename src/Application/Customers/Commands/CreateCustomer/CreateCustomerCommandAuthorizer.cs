using CleanCRM.Application.Common.Authorization;

namespace CleanCRM.Application.Customers.Commands.CreateCustomer;

public class CreateCustomerCommandAuthorizer : AbstractRequestAuthorizer<CreateCustomerCommand>
{
    public override void BuildPolicy(CreateCustomerCommand request)
    {
        UseAllRequirement(new MustBeAuthorizedUserRequirement());
    }
}
