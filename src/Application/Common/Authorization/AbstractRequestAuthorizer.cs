using CleanCRM.Application.Common.Interfaces;

namespace CleanCRM.Application.Common.Authorization;

public abstract class AbstractRequestAuthorizer<TRequest> : IAuthorizer<TRequest>
{
    private readonly IList<IAuthorizationRequirement> _all = new List<IAuthorizationRequirement>();
    public IList<IAuthorizationRequirement> AllRequirements => _all;

    protected void UseAllRequirement(IAuthorizationRequirement requirement)
    {
        _all.Add(requirement);
    }
    public abstract void BuildPolicy(TRequest request);
}
