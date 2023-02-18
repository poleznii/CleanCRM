namespace CleanCRM.Application.Common.Interfaces;

public interface IIdentityService
{
    Task<string?> Login(string userName, string password);
}
