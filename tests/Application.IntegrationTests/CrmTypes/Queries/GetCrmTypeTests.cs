using CleanCRM.Application.Common.Exceptions;
using CleanCRM.Application.CrmTypes.Commands.CreateCrmType;
using CleanCRM.Application.CrmTypes.Queries.GetCrmType;
using CleanCRM.Application.IntegrationTests.CrmTypes.Common;

namespace CleanCRM.Application.IntegrationTests.CrmTypes.Queries;

using static Tests;

internal class GetCrmTypeTests : BaseTestFixture
{
    [Test]
    public async Task ShouldBeAuthorizedUser()
    {
        var command = new GetCrmTypeQuery();

        await FluentActions.Invoking(() => SendAsync(command)).Should().ThrowAsync<UnauthorizedAccessException>();
    }

    [Test]
    public async Task ShouldBeExistingType()
    {
        await RunAsTestUser();

        var query = new GetCrmTypeQuery()
        {
            Id = "customer"
        };

        await FluentActions.Invoking(() => SendAsync(query)).Should().ThrowAsync<NotFoundException>();
    }

    [Test]
    public async Task ShouldGetTypeWithNoFields()
    {
        await RunAsTestUser();

        var typeId = await SendAsync(new CreateCrmTypeCommand() { Id = "customer" });

        var query = new GetCrmTypeQuery()
        {
            Id = typeId.Result
        };

        var entity = await SendAsync(query);

        entity.Result.Should().NotBeNull();
        entity.Result!.Id.Should().Be(typeId.Result);
        entity.Result!.Fields.Should().BeEmpty();
    }


    [Test]
    public async Task ShouldGetTypeWithAllFields()
    {
        await RunAsTestUser();

        var typeId = await SendAsync(CrmTypeTests.GetCreateTypeCommand());

        var query = new GetCrmTypeQuery()
        {
            Id = typeId.Result
        };

        var entity = await SendAsync(query);

        entity.Result.Should().NotBeNull();
        entity.Result!.Id.Should().Be(typeId.Result);
        entity.Result!.Fields.Should().HaveCount(3);
    }

}
