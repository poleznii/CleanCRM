using CleanCRM.Application.Common.Exceptions;
using CleanCRM.Application.CrmItems.Commands.CreateCrmItem;
using CleanCRM.Application.CrmItems.Common;
using CleanCRM.Application.CrmItems.Queries.GetCrmItem;
using CleanCRM.Application.IntegrationTests.CrmTypes.Common;

namespace CleanCRM.Application.IntegrationTests.CrmItems.Commands;

using static Tests;

internal class CreateCrmItemTests : BaseTestFixture
{
    [Test]
    public async Task ShouldBeAuthorizedUser()
    {
        var command = new CreateCrmItemCommand();

        await FluentActions.Invoking(() => SendAsync(command)).Should().ThrowAsync<UnauthorizedAccessException>();
    }

    [Test]
    public async Task ShouldBeNotEmptyRequest()
    {
        await RunAsTestUser();

        var command = new CreateCrmItemCommand();

        await FluentActions.Invoking(() => SendAsync(command)).Should().ThrowAsync<ValidatorException>();
    }

    [Test]
    public async Task ShouldBeExistingType()
    {
        await RunAsTestUser();

        var command = new CreateCrmItemCommand
        {
            Type = "customer"
        };

        await FluentActions.Invoking(() => SendAsync(command)).Should().ThrowAsync<ValidatorException>();
    }

    [Test]
    public async Task ShouldCreateCrmItemWithNoFields()
    {
        await RunAsTestUser();

        var typeId = await SendAsync(CrmTypeTests.GetCreateTypeCommand());

        var entity = await SendAsync(new CreateCrmItemCommand
        {
            Type = typeId.Result
        });

        entity.Result.Should().NotBeNull();
    }

    [Test]
    public async Task ShouldCreateCrmItemWithOneField()
    {
        await RunAsTestUser();

        var typeId = await SendAsync(CrmTypeTests.GetCreateTypeCommand());

        var createCommand = new CreateCrmItemCommand
        {
            Type = typeId.Result,
            Fields = new Dictionary<string, CrmItemFieldDto>()
            {
                { "title", new CrmItemFieldDto() { Values = { "Some title" } } },
                { "not_exists_field1", new CrmItemFieldDto() { Values = { "val 1" } } },
                { "not_exists_field2", new CrmItemFieldDto() { Values = { "val 2" } } }
            }
        };

        var id = await SendAsync(createCommand);

        var entity = await SendAsync(new GetCrmItemQuery()
        {
            Type = typeId.Result,
            Id = id.Result
        });

        entity.Result.Should().NotBeNull();
        entity.Result!.Id.Should().Be(id.Result);
        entity.Result!.Type.Should().Be(typeId.Result);
        entity.Result!.Fields.ContainsKey("title").Should().BeTrue();
        entity.Result!.Fields["title"].Should().NotBeNull();
        entity.Result!.Fields["title"]!.Values.Should().NotBeNull();
        entity.Result!.Fields["title"]!.Values!.Should().HaveCount(1);
        entity.Result!.Fields["title"]!.Values!.Should().Contain("Some title");

        entity.Result!.Fields.ContainsKey("not_exists_field1").Should().BeFalse();
        entity.Result!.Fields.ContainsKey("not_exists_field2").Should().BeFalse();
    }



    [Test]
    public async Task ShouldCreateCrmItemWithAllFields()
    {
        await RunAsTestUser();

        var typeId = await SendAsync(CrmTypeTests.GetCreateTypeCommand());

        var createCommand = new CreateCrmItemCommand
        {
            Type = typeId.Result,
            Fields = new Dictionary<string, CrmItemFieldDto>()
            {
                { "title", new CrmItemFieldDto() { Values = { "Some title" } } },
                { "description", new CrmItemFieldDto() { Values = { "Some description" } } },
                { "views", new CrmItemFieldDto() { Values = { "10000" } } },

            }
        };

        var id = await SendAsync(createCommand);

        var entity = await SendAsync(new GetCrmItemQuery()
        {
            Type = typeId.Result,
            Id = id.Result
        });

        entity.Result.Should().NotBeNull();
        entity.Result!.Id.Should().Be(id.Result);
        entity.Result!.Type.Should().Be(typeId.Result);
        entity.Result!.Fields.ContainsKey("title").Should().BeTrue();
        entity.Result!.Fields["title"].Should().NotBeNull();
        entity.Result!.Fields["title"]!.Values.Should().NotBeNull();
        entity.Result!.Fields["title"]!.Values!.Should().HaveCount(1);
        entity.Result!.Fields["title"]!.Values!.Should().Contain("Some title");


        entity.Result!.Fields.ContainsKey("description").Should().BeTrue();
        entity.Result!.Fields["description"].Should().NotBeNull();
        entity.Result!.Fields["description"]!.Values.Should().NotBeNull();
        entity.Result!.Fields["description"]!.Values!.Should().HaveCount(1);
        entity.Result!.Fields["description"]!.Values!.Should().Contain("Some description");


        entity.Result!.Fields.ContainsKey("views").Should().BeTrue();
        entity.Result!.Fields["views"].Should().NotBeNull();
        entity.Result!.Fields["views"]!.Values.Should().NotBeNull();
        entity.Result!.Fields["views"]!.Values!.Should().HaveCount(1);
        entity.Result!.Fields["views"]!.Values!.Should().Contain("10000");
    }
}
