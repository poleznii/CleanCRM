using CleanCRM.Application.Common.Authorization;

namespace CleanCRM.Application.CrmItems.Commands.CreateCrmItem;

public class CreateCrmItemCommandAuthorizer : AbstractRequestAuthorizer<CreateCrmItemCommand>
{
    public override void BuildPolicy(CreateCrmItemCommand request)
    {
        UseAllRequirement(new MustBeAuthorizedUserRequirement());
    }
}
