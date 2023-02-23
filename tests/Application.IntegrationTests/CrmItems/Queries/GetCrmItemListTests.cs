using CleanCRM.Application.Common.Exceptions;
using CleanCRM.Application.CrmItems.Commands.CreateCrmItem;
using CleanCRM.Application.CrmItems.Common;
using CleanCRM.Application.CrmItems.Queries.GetCrmItemList;
using CleanCRM.Application.IntegrationTests.CrmTypes.Common;

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
    public async Task ShouldBeExistingType()
    {
        await RunAsTestUser();

        var query = new GetCrmItemListQuery()
        {
            Type = "customer"
        };

        await FluentActions.Invoking(() => SendAsync(query)).Should().ThrowAsync<ValidatorException>();
    }

    [Test]
    public async Task ShouldBeValidListParams()
    {
        await RunAsTestUser();

        var typeId = await SendAsync(CrmTypeTests.GetCreateTypeCommand());

        var query = new GetCrmItemListQuery()
        {
            Type = typeId.Result,
            Skip = -1,
            Take = -1
        };

        await FluentActions.Invoking(() => SendAsync(query)).Should().ThrowAsync<ValidatorException>();
    }

    [Test]
    public async Task ShouldReturnEmptyList()
    {
        await RunAsTestUser();

        var typeId = await SendAsync(CrmTypeTests.GetCreateTypeCommand());

        var query = new GetCrmItemListQuery()
        {
            Type = typeId.Result,
        };

        var list = await SendAsync(query);

        list.Items.Should().NotBeNull();
        list.Items!.Should().HaveCount(0);
    }

    [Test]
    public async Task ShouldReturnOneItem()
    {
        await RunAsTestUser();

        var typeId = await SendAsync(CrmTypeTests.GetCreateTypeCommand());

        var id = await SendAsync(new CreateCrmItemCommand()
        {
            Type = typeId.Result,
            Fields = new Dictionary<string, CrmItemFieldDto>()
            {
                { "title", new CrmItemFieldDto() { Values = { "Some title" } } },
                { "not_exists_field1", new CrmItemFieldDto() { Values = { "val 1" } } },
                { "not_exists_field2", new CrmItemFieldDto() { Values = { "val 2" } } }
            }
        });

        var query = new GetCrmItemListQuery()
        {
            Type = typeId.Result,
        };

        var list = await SendAsync(query);

        list.Items.Should().NotBeNull();
        list.Items!.Should().HaveCount(1);

        var entity = list.Items.First();
        entity.Id.Should().Be(id.Result);
        entity.Type.Should().Be(typeId.Result);
        entity.Fields.ContainsKey("title").Should().BeTrue();
        entity.Fields["title"].Should().NotBeNull();
        entity.Fields["title"]!.Values.Should().NotBeNull();
        entity.Fields["title"]!.Values!.Should().HaveCount(1);
        entity.Fields["title"]!.Values!.Should().Contain("Some title");

        entity.Fields.ContainsKey("not_exists_field1").Should().BeFalse();
        entity.Fields.ContainsKey("not_exists_field2").Should().BeFalse();
    }

    [Test]
    public async Task ShouldReturnAllItems()
    {
        await RunAsTestUser();

        var typeId = await SendAsync(CrmTypeTests.GetCreateTypeCommand());

        await SendAsync(new CreateCrmItemCommand()
        {
            Type = typeId.Result
        });

        await SendAsync(new CreateCrmItemCommand()
        {
            Type = typeId.Result
        });

        await SendAsync(new CreateCrmItemCommand()
        {
            Type = typeId.Result
        });
        var query = new GetCrmItemListQuery()
        {
            Type = typeId.Result,
        };

        var list = await SendAsync(query);

        list.Items.Should().NotBeNull();
        list.Items!.Should().HaveCount(3);
    }
}
