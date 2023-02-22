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
}
