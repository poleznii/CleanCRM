using CleanCRM.Application.Common.Interfaces;
using FluentValidation;
using Microsoft.EntityFrameworkCore;

namespace CleanCRM.Application.CrmItems.Queries.GetCrmItemList;

public class GetCrmItemListQueryValidator : AbstractValidator<GetCrmItemListQuery>
{
    private readonly IApplicationDbContext _context;

    public GetCrmItemListQueryValidator(IApplicationDbContext context)
    {
        _context = context;

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

    //TODO cache
    public async Task<bool> BeInListOfTypes(GetCrmItemListQuery model, string type, CancellationToken cancellationToken)
    {
        return await _context.CrmTypes.AnyAsync(l => l.Id.Equals(type), cancellationToken);
    }
}
