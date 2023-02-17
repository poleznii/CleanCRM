using CleanCRM.Application.Customers.Common;
using MediatR;

namespace CleanCRM.Application.Customers.Queries.GetCustomer;

public record GetCustomerQuery : IRequest<CustomerDto>
{
    public int Id { get; set; }
}


public class GetCustomerQueryHandler : IRequestHandler<GetCustomerQuery, CustomerDto>
{
    public async Task<CustomerDto> Handle(GetCustomerQuery request, CancellationToken cancellationToken)
    {
        return new CustomerDto(request.Id, "Customer Name");
    }
}