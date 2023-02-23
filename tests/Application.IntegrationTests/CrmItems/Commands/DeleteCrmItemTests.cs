using CleanCRM.Application.Common.Exceptions;
using CleanCRM.Application.CrmItems.Commands.CreateCrmItem;
using CleanCRM.Application.CrmItems.Commands.DeleteCrmItem;
using CleanCRM.Application.CrmItems.Queries.GetCrmItem;
using CleanCRM.Application.IntegrationTests.CrmTypes.Common;

namespace CleanCRM.Application.IntegrationTests.CrmItems.Commands;

using static Tests;

internal class DeleteCrmItemTests : BaseTestFixture
{
    [Test]
    public async Task ShouldBeAuthorizedUser()
    {
        var command = new DeleteCrmItemCommand();

        await FluentActions.Invoking(() => SendAsync(command)).Should().ThrowAsync<UnauthorizedAccessException>();
    }

    [Test]
    public async Task ShouldBeNotEmptyRequest()
    {
        await RunAsTestUser();

        var command = new DeleteCrmItemCommand();

        await FluentActions.Invoking(() => SendAsync(command)).Should().ThrowAsync<ValidatorException>();
    }

    [Test]
    public async Task ShouldBeExistingType()
    {
        await RunAsTestUser();

        var command = new DeleteCrmItemCommand()
        {
            Type = "customer",
            Id = "id"
        };

        await FluentActions.Invoking(() => SendAsync(command)).Should().ThrowAsync<ValidatorException>();
    }

    [Test]
    public async Task ShouldBeExistingId()
    {
        await RunAsTestUser();

        var typeId = await SendAsync(CrmTypeTests.GetCreateTypeCommand());

        var command = new DeleteCrmItemCommand()
        {
            Type = typeId.Result,
            Id = "id"
        };

        await FluentActions.Invoking(() => SendAsync(command)).Should().ThrowAsync<NotFoundException>();
    }

    [Test]
    public async Task ShouldDeleteCrmItem()
    {
        await RunAsTestUser();

        var typeId = await SendAsync(CrmTypeTests.GetCreateTypeCommand());

        var id = await SendAsync(new CreateCrmItemCommand() { Type = typeId.Result });

        var command = new DeleteCrmItemCommand()
        {
            Type = typeId.Result,
            Id = id.Result
        };

        var result = await SendAsync(command);

        result.Should().NotBeNull();
        result!.Result.Should().BeTrue();

        await FluentActions.Invoking(() => SendAsync(new GetCrmItemQuery() { Type = typeId.Result, Id = id.Result })).Should().ThrowAsync<NotFoundException>();
    }
}
