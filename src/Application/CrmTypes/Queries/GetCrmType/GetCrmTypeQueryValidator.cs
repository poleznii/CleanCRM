using FluentValidation;

namespace CleanCRM.Application.CrmTypes.Queries.GetCrmType;

public class GetCrmTypeQueryValidator : AbstractValidator<GetCrmTypeQuery>
{
    public GetCrmTypeQueryValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty();
    }
}
