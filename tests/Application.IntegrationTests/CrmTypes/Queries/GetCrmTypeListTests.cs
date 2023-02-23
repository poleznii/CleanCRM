using CleanCRM.Application.Common.Exceptions;
using CleanCRM.Application.CrmTypes.Commands.CreateCrmType;
using CleanCRM.Application.CrmTypes.Queries.GetCrmTypeList;
using CleanCRM.Application.IntegrationTests.CrmTypes.Common;
using CleanCRM.Domain.ValueObjects;


namespace CleanCRM.Application.IntegrationTests.CrmTypes.Queries;

using static Tests;

internal class GetCrmTypeListTests : BaseTestFixture
{
    [Test]
    public async Task ShouldBeAuthorizedUser()
    {
        var command = new GetCrmTypeListQuery();

        await FluentActions.Invoking(() => SendAsync(command)).Should().ThrowAsync<UnauthorizedAccessException>();
    }

    [Test]
    public async Task ShouldBeValidListParams()
    {
        await RunAsTestUser();

        var query = new GetCrmTypeListQuery()
        {
            Skip = -1,
            Take = -1
        };

        await FluentActions.Invoking(() => SendAsync(query)).Should().ThrowAsync<ValidatorException>();
    }

    [Test]
    public async Task ShouldReturnEmptyList()
    {
        await RunAsTestUser();

        var query = new GetCrmTypeListQuery();

        var list = await SendAsync(query);

        list.Items.Should().NotBeNull();
        list.Items!.Should().HaveCount(0);
    }


    [Test]
    public async Task ShouldReturnOneItem()
    {
        await RunAsTestUser();

        var typeId = await SendAsync(CrmTypeTests.GetCreateTypeCommand());

        var query = new GetCrmTypeListQuery();

        var list = await SendAsync(query);

        list.Items.Should().NotBeNull();
        list.Items!.Should().HaveCount(1);

        var entity = list.Items.First();
        entity.Id.Should().Be(typeId.Result);
        entity.Fields.Should().HaveCount(3);

        entity.Fields.ContainsKey("title").Should().BeTrue();
        entity.Fields["title"].Should().NotBeNull();
        entity.Fields["title"].Metadata["type"].Should().NotBeNull();
        entity.Fields["title"].Metadata["type"].Should().Be(CrmFieldType.String.ToString());

        entity.Fields.ContainsKey("description").Should().BeTrue();
        entity.Fields["description"].Should().NotBeNull();
        entity.Fields["description"].Metadata["type"].Should().NotBeNull();
        entity.Fields["description"].Metadata["type"].Should().Be(CrmFieldType.String.ToString());

        entity.Fields.ContainsKey("views").Should().BeTrue();
        entity.Fields["views"].Should().NotBeNull();
        entity.Fields["views"].Metadata["type"].Should().NotBeNull();
        entity.Fields["views"].Metadata["type"].Should().Be(CrmFieldType.Integer.ToString());
    }


    [Test]
    public async Task ShouldReturnAllItems()
    {
        await RunAsTestUser();

        await SendAsync(new CreateCrmTypeCommand() { Id = "customer1" });
        await SendAsync(new CreateCrmTypeCommand() { Id = "customer2" });
        await SendAsync(new CreateCrmTypeCommand() { Id = "customer3" });

        var query = new GetCrmTypeListQuery();

        var list = await SendAsync(query);

        list.Items.Should().NotBeNull();
        list.Items!.Should().HaveCount(3);
    }
}
