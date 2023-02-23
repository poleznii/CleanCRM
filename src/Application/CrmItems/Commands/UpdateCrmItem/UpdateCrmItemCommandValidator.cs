using CleanCRM.Application.Common.Interfaces;
using CleanCRM.Application.Common.Validators;
using FluentValidation;

namespace CleanCRM.Application.CrmItems.Commands.UpdateCrmItem;

public class UpdateCrmItemCommandValidator : CrmItemValidator<UpdateCrmItemCommand>
{
    public UpdateCrmItemCommandValidator(IApplicationDbContext context) : base(context)
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
