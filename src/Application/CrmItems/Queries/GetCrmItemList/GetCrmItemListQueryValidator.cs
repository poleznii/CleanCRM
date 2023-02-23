using CleanCRM.Application.Common.Interfaces;
using CleanCRM.Application.Common.Validators;
using FluentValidation;

namespace CleanCRM.Application.CrmItems.Queries.GetCrmItemList;

public class GetCrmItemListQueryValidator : CrmItemValidator<GetCrmItemListQuery>
{
    public GetCrmItemListQueryValidator(IApplicationDbContext context) : base(context)
    {
        RuleFor(x => x.Type)
            .NotEmpty();

        When(x => !string.IsNullOrEmpty(x.Type), () =>
        {
            RuleFor(x => x.Type)
                .MustAsync(BeInListOfTypes);
        });

        RuleFor(x => x.Skip)
            .GreaterThanOrEqualTo(0);

        RuleFor(x => x.Take)
            .GreaterThanOrEqualTo(1);
    }
}
