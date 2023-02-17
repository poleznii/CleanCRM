using Microsoft.AspNetCore.Mvc;

namespace CleanCRM.ApiUI.Controllers;

public class CustomerController : ApiControllerBase
{
    [HttpGet("get/{id}")]
    public string Get(int id)
    {
        return $"value {id}";
    }
}
