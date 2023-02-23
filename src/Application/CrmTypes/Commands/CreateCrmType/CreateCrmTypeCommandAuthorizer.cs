using CleanCRM.Application.Common.Authorization;

namespace CleanCRM.Application.CrmTypes.Commands.CreateCrmType;

public class CreateCrmTypeCommandAuthorizer : AbstractRequestAuthorizer<CreateCrmTypeCommand>
{
    public override void BuildPolicy(CreateCrmTypeCommand request)
    {
        UseAllRequirement(new MustBeAuthorizedUserRequirement());
    }
}
