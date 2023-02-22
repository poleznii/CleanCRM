using CleanCRM.Application.Common.Interfaces;
using FluentValidation;
using Microsoft.EntityFrameworkCore;

namespace CleanCRM.Application.CrmTypes.Commands.CreateCrmType;

public class CreateCrmTypeCommandValidator : AbstractValidator<CreateCrmTypeCommand>
{
    private readonly IApplicationDbContext _context;
    public CreateCrmTypeCommandValidator(IApplicationDbContext context)
    {
        _context = context;

        RuleFor(x => x.Id)
            .MaximumLength(100)
            .NotEmpty();

        When(x => !string.IsNullOrEmpty(x.Id), () =>
        {
            RuleFor(x => x.Id)
                .MustAsync(MustBeUniqueId);
        });
    }

    private async Task<bool> MustBeUniqueId(string id, CancellationToken cancellationToken)
    {
        return !await _context.CrmTypes.AnyAsync(x => x.Id.Equals(id), cancellationToken);
    }
}
