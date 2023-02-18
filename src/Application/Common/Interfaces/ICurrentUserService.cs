namespace CleanCRM.Application.Common.Interfaces;

public interface ICurrentUserService
{
    public string? UserId { get; }
    public string? UserName { get; }
}
