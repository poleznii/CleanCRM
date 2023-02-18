using CleanCRM.Application.Customers.Commands.CreateCustomer;
using CleanCRM.Application.Customers.Commands.DeleteCustomer;
using CleanCRM.Application.Customers.Commands.UpdateCustomer;
using CleanCRM.Application.Customers.Queries.GetCustomer;
using CleanCRM.Application.Customers.Queries.GetCustomerList;
using Microsoft.AspNetCore.Mvc;

namespace CleanCRM.ApiUI.Controllers;

public class CustomerController : ApiControllerBase
{
    [HttpGet("list")]
    public async Task<ActionResult<IApiResult>> GetList([FromQuery] GetCustomerListQuery query)
    {
        return await Mediator.Send(query);
    }

    [HttpGet("get/{id}")]
    public async Task<ActionResult<IApiResult>> Get([FromRoute] GetCustomerQuery query)
    {
        return await Mediator.Send(query);
    }

    [HttpPost("create")]
    public async Task<ActionResult<IApiResult>> Create([FromBody] CreateCustomerCommand command)
    {
        return await Mediator.Send(command);
    }

    [HttpPut("update")]
    public async Task<ActionResult<IApiResult>> Update([FromBody] UpdateCustomerCommand command)
    {
        return await Mediator.Send(command);
    }

    [HttpDelete("delete/{id}")]
    public async Task<ActionResult<IApiResult>> Delete([FromRoute] DeleteCustomerCommand command)
    {
        return await Mediator.Send(command);
    }
}
