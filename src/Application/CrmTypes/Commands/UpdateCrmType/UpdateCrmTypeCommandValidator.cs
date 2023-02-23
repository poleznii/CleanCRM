using CleanCRM.Application.Common.Interfaces;
using FluentValidation;
using Microsoft.EntityFrameworkCore;

namespace CleanCRM.Application.CrmTypes.Commands.UpdateCrmType;

public class UpdateCrmTypeCommandValidator : AbstractValidator<UpdateCrmTypeCommand>
{
    private readonly IApplicationDbContext _context;

    public UpdateCrmTypeCommandValidator(IApplicationDbContext context)
    {
        _context = context;

        RuleFor(x => x.Id)
            .NotEmpty();

        When(x => !string.IsNullOrEmpty(x.Id), () =>
        {
            RuleFor(x => x.Id)
                .MustAsync(MustBeExistingId);
        });
    }

    private async Task<bool> MustBeExistingId(string id, CancellationToken cancellationToken)
    {
        return await _context.CrmTypes.AnyAsync(x => x.Id.Equals(id), cancellationToken);
    }
}
