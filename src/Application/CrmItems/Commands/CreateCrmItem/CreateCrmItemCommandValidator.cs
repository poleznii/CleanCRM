using CleanCRM.Application.Common.Interfaces;
using CleanCRM.Application.Common.Validators;
using FluentValidation;

namespace CleanCRM.Application.CrmItems.Commands.CreateCrmItem;

public class CreateCrmItemCommandValidator : CrmItemValidator<CreateCrmItemCommand>
{
    public CreateCrmItemCommandValidator(IApplicationDbContext context) : base(context)
    {
        RuleFor(x => x.Type)
            .NotEmpty();

        When(x => !string.IsNullOrEmpty(x.Type), () =>
        {
            RuleFor(x => x.Type)
                .MustAsync(BeInListOfTypes);
        });
    }
}
