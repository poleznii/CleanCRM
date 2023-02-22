using CleanCRM.Application.Common.Exceptions;
using CleanCRM.Application.CrmItems.Queries.GetCrmItemList;

namespace CleanCRM.Application.IntegrationTests.CrmItems.Queries;

using static Tests;

internal class GetCrmItemListTests : BaseTestFixture
{
    [Test]
    public async Task ShouldBeAuthorizedUser()
    {
        var command = new GetCrmItemListQuery();

        await FluentActions.Invoking(() => SendAsync(command)).Should().ThrowAsync<UnauthorizedAccessException>();
    }

    [Test]
    public async Task ShouldBeValidListParams()
    {
        await RunAsTestUser();

        var query = new GetCrmItemListQuery()
        {
            Skip = -1,
            Take = -1
        };

        await FluentActions.Invoking(() => SendAsync(query)).Should().ThrowAsync<ValidatorException>();
    }

    [Test]
    public async Task ShouldBeExistingType()
    {
        await RunAsTestUser();

        var query = new GetCrmItemListQuery()
        {
            Type = "testtype",
        };

        await FluentActions.Invoking(() => SendAsync(query)).Should().ThrowAsync<ValidatorException>();
    }
}
