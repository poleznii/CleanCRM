using CleanCRM.Application.Common.Interfaces;
using CleanCRM.Application.Common.Models;

namespace CleanCRM.Application.Common.Authorization;

public class MustBeAuthorizedUserRequirement : IAuthorizationRequirement
{
    class MustBeAuthorizedUserRequirementHandler : IAuthorizationHandler<MustBeAuthorizedUserRequirement>
    {
        private readonly ICurrentUserService _currentUserService;

        public MustBeAuthorizedUserRequirementHandler(ICurrentUserService currentUserService)
        {
            _currentUserService = currentUserService;
        }

        public async Task<Result> Handle(MustBeAuthorizedUserRequirement request, CancellationToken cancellationToken)
        {
            if (!string.IsNullOrEmpty(_currentUserService.UserId))
            {
                return Result.Success();
            }
            return Result.Fail("You are not authorized to perform this action.");
        }
    }
}
