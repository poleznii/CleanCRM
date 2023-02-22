using CleanCRM.Application.Common.Authorization;

namespace CleanCRM.Application.CrmItems.Commands.UpdateCrmItem;

public class UpdateCrmItemCommandAuthorizer : AbstractRequestAuthorizer<UpdateCrmItemCommand>
{
    public override void BuildPolicy(UpdateCrmItemCommand request)
    {
        UseAllRequirement(new MustBeAuthorizedUserRequirement());
    }
}
