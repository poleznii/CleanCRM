using CleanCRM.Application.Common.Authorization;

namespace CleanCRM.Application.CrmItems.Commands.DeleteCrmItem;

public class DeleteCrmItemCommandAuthorizer : AbstractRequestAuthorizer<DeleteCrmItemCommand>
{
    public override void BuildPolicy(DeleteCrmItemCommand request)
    {
        UseAllRequirement(new MustBeAuthorizedUserRequirement());
    }
}
