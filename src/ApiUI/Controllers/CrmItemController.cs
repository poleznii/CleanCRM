using CleanCRM.Application.CrmItems.Commands.CreateCrmItem;
using CleanCRM.Application.CrmItems.Commands.DeleteCrmItem;
using CleanCRM.Application.CrmItems.Commands.UpdateCrmItem;
using CleanCRM.Application.CrmItems.Queries.GetCrmItem;
using CleanCRM.Application.CrmItems.Queries.GetCrmItemList;
using Microsoft.AspNetCore.Mvc;

namespace CleanCRM.ApiUI.Controllers;

public class CrmItemController : ApiControllerBase
{
    [HttpGet("{type}/list")]
    public async Task<ActionResult<IApiResult>> GetList([FromRoute] string type, [FromQuery] GetCrmItemListQuery query)
    {
        query.Type = type;
        return await Mediator.Send(query);
    }

    [HttpGet("{type}/get/{id}")]
    public async Task<ActionResult<IApiResult>> Get([FromRoute] GetCrmItemQuery query)
    {
        return await Mediator.Send(query);
    }

    [HttpPost("{type}/create")]
    public async Task<ActionResult<IApiResult>> Create([FromRoute] string type, [FromBody] CreateCrmItemCommand command)
    {
        command.Type = type;
        return await Mediator.Send(command);
    }

    [HttpPut("{type}/update")]
    public async Task<ActionResult<IApiResult>> Update([FromRoute] string type, [FromBody] UpdateCrmItemCommand command)
    {
        command.Type = type;
        return await Mediator.Send(command);
    }

    [HttpDelete("{type}/delete/{id}")]
    public async Task<ActionResult<IApiResult>> Delete([FromRoute] DeleteCrmItemCommand command)
    {
        return await Mediator.Send(command);
    }
}
