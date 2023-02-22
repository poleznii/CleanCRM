using CleanCRM.Application.Common.Interfaces;
using FluentValidation;
using Microsoft.EntityFrameworkCore;

namespace CleanCRM.Application.Common.Validators;

public class CrmItemValidator<T> : AbstractValidator<T>
{
    protected IApplicationDbContext _ontext;

    public CrmItemValidator(IApplicationDbContext context)
    {
        _ontext = context;
    }
    
    protected async Task<bool> BeInListOfTypes(string type, CancellationToken cancellationToken)
    {
        return await _ontext.CrmTypes.AnyAsync(l => l.Id.Equals(type), cancellationToken);
    }
}
