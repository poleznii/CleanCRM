namespace CleanCRM.Application.Common.Interfaces;

public interface IAuthorizer<T>
{
    /// <summary>
    /// Success if all requirement is true
    /// </summary>
    IList<IAuthorizationRequirement> AllRequirements { get; }
    void BuildPolicy(T instance);
}
