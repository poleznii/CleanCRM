using CleanCRM.Application.Common.Authorization;

namespace CleanCRM.Application.Customers.Commands.DeleteCustomer;

public class DeleteCustomerCommandAuthorizer : AbstractRequestAuthorizer<DeleteCustomerCommand>
{
    public override void BuildPolicy(DeleteCustomerCommand request)
    {
        UseAllRequirement(new MustBeAuthorizedUserRequirement());
    }
}
