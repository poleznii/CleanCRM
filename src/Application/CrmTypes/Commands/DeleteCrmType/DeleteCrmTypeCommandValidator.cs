using CleanCRM.Application.Common.Interfaces;
using FluentValidation;
using Microsoft.EntityFrameworkCore;

namespace CleanCRM.Application.CrmTypes.Commands.DeleteCrmType;

public class DeleteCrmTypeCommandValidator : AbstractValidator<DeleteCrmTypeCommand>
{
    private readonly IApplicationDbContext _context;
    public DeleteCrmTypeCommandValidator(IApplicationDbContext context)
    {
        _context = context;

        RuleFor(x => x.Id)
            .NotEmpty();

        When(x => !string.IsNullOrEmpty(x.Id), () =>
        {
            RuleFor(x => x.Id)
                .MustAsync(MustDoNotHaveItems);
        });
    }

    private async Task<bool> MustDoNotHaveItems(string id, CancellationToken cancellationToken)
    {
        return !await _context.CrmItems.AnyAsync(x => x.TypeId.Equals(id), cancellationToken);
    }
}
