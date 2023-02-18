using CleanCRM.Application.Common.Interfaces;
using MediatR;

namespace CleanCRM.Application.Common.Behaviours;

public class RequestAuthorizationBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse> where TRequest : notnull, IApiRequest
{
    private readonly IEnumerable<IAuthorizer<TRequest>> _authorizers;
    private readonly IMediator _mediator;

    public RequestAuthorizationBehavior(IEnumerable<IAuthorizer<TRequest>> authorizers, IMediator mediator)
    {
        _authorizers = authorizers;
        _mediator = mediator;
    }

    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        var allRequirements = new List<IAuthorizationRequirement>();

        foreach (var authorizer in _authorizers)
        {
            authorizer.BuildPolicy(request);
            allRequirements.AddRange(authorizer.AllRequirements);
        }

        if (allRequirements.Count > 0)
        {
            await HandleAllRequirements(allRequirements, cancellationToken);
        }

        return await next();
    }
    private async Task HandleAllRequirements(List<IAuthorizationRequirement> requirements, CancellationToken cancellationToken)
    {
        foreach (var requirement in requirements.Distinct())
        {
            var result = await _mediator.Send(requirement, cancellationToken);
            if (!result.Succeeded)
                throw new UnauthorizedAccessException(result.FailureMessage);
        }
    }
}
