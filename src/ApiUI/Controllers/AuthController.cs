using CleanCRM.Application.Authentication.Commands.PasswordLogin;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CleanCRM.ApiUI.Controllers;

public class AuthController : ApiControllerBase
{
    [HttpPost("login")]
    [AllowAnonymous]
    public async Task<ActionResult<IApiResult>> Login([FromBody] PasswordLoginCommand command)
    {
        return await Mediator.Send(command);
    }
}
