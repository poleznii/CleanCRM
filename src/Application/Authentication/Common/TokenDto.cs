namespace CleanCRM.Application.Authentication.Common;

public record TokenDto
{
    public required string AccessToken { get; init; }
    // refresh token someday?
}
