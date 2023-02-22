using FluentValidation;

namespace CleanCRM.Application.CrmTypes.Commands.UpdateCrmType;

public class UpdateCrmTypeCommandValidator : AbstractValidator<UpdateCrmTypeCommand>
{
    public UpdateCrmTypeCommandValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty();
    }
}
