using CleanCRM.Application.Common.Authorization;

namespace CleanCRM.Application.CrmTypes.Commands.DeleteCrmType;

public class DeleteCrmTypeCommandAuthorizer : AbstractRequestAuthorizer<DeleteCrmTypeCommand>
{
    public override void BuildPolicy(DeleteCrmTypeCommand request)
    {
        UseAllRequirement(new MustBeAuthorizedUserRequirement());
    }
}
