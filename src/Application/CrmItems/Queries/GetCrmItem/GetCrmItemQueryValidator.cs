using CleanCRM.Application.Customers.Queries.GetCustomer;
using FluentValidation;

namespace CleanCRM.Application.CrmItems.Queries.GetCrmItem;

public class GetCrmItemQueryValidator : AbstractValidator<GetCrmItemQuery>
{
    public GetCrmItemQueryValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty();
    }
}
