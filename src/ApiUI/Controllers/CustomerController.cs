using CleanCRM.Application.Common.Models;
using CleanCRM.Application.Customers.Common;
using CleanCRM.Application.Customers.Queries.GetCustomer;
using CleanCRM.Application.Customers.Queries.GetCustomerList;
using Microsoft.AspNetCore.Mvc;

namespace CleanCRM.ApiUI.Controllers;

public class CustomerController : ApiControllerBase
{
    [HttpGet("list")]
    public async Task<ListResult<CustomerDto>> GetList([FromQuery] GetCustomerListQuery query)
    {
        return await Mediator.Send(query);
    }

    [HttpGet("get/{id}")]
    public async Task<CustomerDto> Get([FromRoute] GetCustomerQuery query)
    {
        return await Mediator.Send(query);
    }
}
