﻿using CleanCRM.Application.Authentication.Common;
using CleanCRM.Application.Common.Interfaces;
using MediatR;
using System.Security.Authentication;

namespace CleanCRM.Application.Authentication.Commands.PasswordLogin;

public record PasswordLoginCommand : IRequest<TokenDto>
{
    public string UserName { get; set; } = null!;
    public string Password { get; set; } = null!;
}


public class PasswordLoginCommandHandler : IRequestHandler<PasswordLoginCommand, TokenDto>
{
    private readonly IIdentityService _identityService;

    public PasswordLoginCommandHandler(IIdentityService identityService)
    {
        _identityService = identityService;
    }

    public async Task<TokenDto> Handle(PasswordLoginCommand request, CancellationToken cancellationToken)
    {
        var accessToken = await _identityService.Login(request.UserName, request.Password);
        if (accessToken == null)
        {
            throw new AuthenticationException();
        }

        return new TokenDto()
        {
            AccessToken = accessToken
        };
    }
}
