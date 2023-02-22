using CleanCRM.Application.Common.Authorization;

namespace CleanCRM.Application.CrmTypes.Commands.UpdateCrmType;

public class UpdateCrmTypeCommandAuthorizer : AbstractRequestAuthorizer<UpdateCrmTypeCommand>
{
    public override void BuildPolicy(UpdateCrmTypeCommand request)
    {
        UseAllRequirement(new MustBeAuthorizedUserRequirement());
    }
}
