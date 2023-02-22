using CleanCRM.Application.CrmTypes.Commands.CreateCrmType;
using CleanCRM.Application.CrmTypes.Commands.DeleteCrmType;
using CleanCRM.Application.CrmTypes.Commands.UpdateCrmType;
using CleanCRM.Application.CrmTypes.Queries.GetCrmType;
using CleanCRM.Application.CrmTypes.Queries.GetCrmTypeList;
using Microsoft.AspNetCore.Mvc;

namespace CleanCRM.ApiUI.Controllers;

public class CrmTypeController : ApiControllerBase
{
    [HttpGet("list")]
    public async Task<ActionResult<IApiResult>> GetList([FromQuery] GetCrmTypeListQuery query)
    {
        return await Mediator.Send(query);
    }

    [HttpGet("get/{id}")]
    public async Task<ActionResult<IApiResult>> Get([FromRoute] GetCrmTypeQuery query)
    {
        return await Mediator.Send(query);
    }

    [HttpPost("create")]
    public async Task<ActionResult<IApiResult>> Create([FromBody] CreateCrmTypeCommand command)
    {
        return await Mediator.Send(command);
    }


    [HttpPut("update")]
    public async Task<ActionResult<IApiResult>> Update([FromBody] UpdateCrmTypeCommand command)
    {
        return await Mediator.Send(command);
    }


    [HttpDelete("delete/{id}")]
    public async Task<ActionResult<IApiResult>> Delete([FromRoute] DeleteCrmTypeCommand command)
    {
        return await Mediator.Send(command);
    }
}
