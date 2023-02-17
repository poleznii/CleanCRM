using CleanCRM.Application.Customers.Common;
using CleanCRM.Application.Customers.Queries.GetCustomer;
using Microsoft.AspNetCore.Mvc;

namespace CleanCRM.ApiUI.Controllers;

public class CustomerController : ApiControllerBase
{
    [HttpGet("get/{id}")]
    public async Task<CustomerDto> Get([FromRoute] GetCustomerQuery query)
    {
        return await Mediator.Send(query);
    }
}
