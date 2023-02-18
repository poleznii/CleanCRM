using CleanCRM.Application.Authentication.Commands.PasswordLogin;
using CleanCRM.Application.Authentication.Common;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CleanCRM.ApiUI.Controllers;

public class AuthController : ApiControllerBase
{
    [HttpPost("login")]
    [AllowAnonymous]
    public async Task<ActionResult<TokenDto>> Login([FromBody] PasswordLoginCommand command)
    {
        return await Mediator.Send(command);
    }
}
