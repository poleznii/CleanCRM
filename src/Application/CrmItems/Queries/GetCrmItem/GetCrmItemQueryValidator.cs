using CleanCRM.Application.Common.Interfaces;
using CleanCRM.Application.Common.Validators;
using FluentValidation;

namespace CleanCRM.Application.CrmItems.Queries.GetCrmItem;

public class GetCrmItemQueryValidator : CrmItemValidator<GetCrmItemQuery>
{
    public GetCrmItemQueryValidator(IApplicationDbContext context) : base(context)
    {
        RuleFor(x => x.Id)
            .NotEmpty();

        RuleFor(x => x.Type)
            .NotEmpty();

        When(x => !string.IsNullOrEmpty(x.Type), () =>
        {
            RuleFor(x => x.Type)
                .MustAsync(BeInListOfTypes);
        });
    }
}
