using CleanCRM.Application.Common.Exceptions;
using CleanCRM.Application.CrmItems.Commands.CreateCrmItem;
using CleanCRM.Application.CrmTypes.Commands.DeleteCrmType;
using CleanCRM.Application.CrmTypes.Queries.GetCrmType;
using CleanCRM.Application.IntegrationTests.CrmTypes.Common;

namespace CleanCRM.Application.IntegrationTests.CrmTypes.Commands;

using static Tests;

internal class DeleteCrmTypeTests : BaseTestFixture
{
    [Test]
    public async Task ShouldBeAuthorizedUser()
    {
        var command = new DeleteCrmTypeCommand();

        await FluentActions.Invoking(() => SendAsync(command)).Should().ThrowAsync<UnauthorizedAccessException>();
    }

    [Test]
    public async Task ShouldBeNotEmptyRequest()
    {
        await RunAsTestUser();

        var command = new DeleteCrmTypeCommand();

        await FluentActions.Invoking(() => SendAsync(command)).Should().ThrowAsync<ValidatorException>();
    }

    [Test]
    public async Task ShouldBeExistingCrmType()
    {
        await RunAsTestUser();

        var command = new DeleteCrmTypeCommand()
        {
            Id = "customer"
        };

        await FluentActions.Invoking(() => SendAsync(command)).Should().ThrowAsync<NotFoundException>();
    }

    [Test]
    public async Task ShoulNotDeleteTypeIfItemsExists()
    {
        await RunAsTestUser();

        var typeId = await SendAsync(CrmTypeTests.GetCreateTypeCommand());

        await SendAsync(new CreateCrmItemCommand() { Type = typeId.Result });

        var command = new DeleteCrmTypeCommand()
        {
            Id = typeId.Result
        };

        await FluentActions.Invoking(() => SendAsync(command)).Should().ThrowAsync<ValidatorException>();
    }

    [Test]
    public async Task ShouldDeleteCrmType()
    {
        await RunAsTestUser();

        var id = await SendAsync(CrmTypeTests.GetCreateTypeCommand());

        var command = new DeleteCrmTypeCommand()
        {
            Id = id.Result
        };

        var result = await SendAsync(command);

        result.Should().NotBeNull();
        result!.Result.Should().BeTrue();

        await FluentActions.Invoking(() => SendAsync(new GetCrmTypeQuery() { Id = id.Result })).Should().ThrowAsync<NotFoundException>();
    }
}
