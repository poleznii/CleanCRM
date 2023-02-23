using FluentValidation;

namespace CleanCRM.Application.CrmTypes.Queries.GetCrmTypeList;

public class GetCrmTypeListQueryValidator : AbstractValidator<GetCrmTypeListQuery>
{
    public GetCrmTypeListQueryValidator()
    {
        RuleFor(x => x.Skip)
            .GreaterThanOrEqualTo(0);

        RuleFor(x => x.Take)
            .GreaterThanOrEqualTo(1);
    }
}
