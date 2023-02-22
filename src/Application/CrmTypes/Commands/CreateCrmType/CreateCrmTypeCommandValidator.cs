using FluentValidation;

namespace CleanCRM.Application.CrmTypes.Commands.CreateCrmType;

public class CreateCrmTypeCommandValidator : AbstractValidator<CreateCrmTypeCommand>
{
    public CreateCrmTypeCommandValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty();
    }
}
