using CleanCRM.Application.Common.Exceptions;
using CleanCRM.Application.CrmItems.Commands.CreateCrmItem;
using CleanCRM.Application.CrmItems.Commands.UpdateCrmItem;
using CleanCRM.Application.CrmItems.Common;
using CleanCRM.Application.CrmItems.Queries.GetCrmItem;
using CleanCRM.Application.IntegrationTests.CrmTypes.Common;

namespace CleanCRM.Application.IntegrationTests.CrmItems.Commands;

using static Tests;

internal class UpdateCrmItemTests : BaseTestFixture
{
    [Test]
    public async Task ShouldBeAuthorizedUser()
    {
        var command = new UpdateCrmItemCommand();

        await FluentActions.Invoking(() => SendAsync(command)).Should().ThrowAsync<UnauthorizedAccessException>();
    }

    [Test]
    public async Task ShouldBeNotEmptyRequest()
    {
        await RunAsTestUser();

        var command = new UpdateCrmItemCommand();

        await FluentActions.Invoking(() => SendAsync(command)).Should().ThrowAsync<ValidatorException>();
    }

    [Test]
    public async Task ShouldBeExistingType()
    {
        await RunAsTestUser();

        var command = new UpdateCrmItemCommand
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

        var command = new UpdateCrmItemCommand()
        {
            Id = "id",
            Type = typeId.Result
        };

        await FluentActions.Invoking(() => SendAsync(command)).Should().ThrowAsync<NotFoundException>();
    }

    [Test]
    public async Task ShouldUpdateCrmItemWithNoFields()
    {
        await RunAsTestUser();

        var typeId = await SendAsync(CrmTypeTests.GetCreateTypeCommand());

        var createdEntity = await SendAsync(new CreateCrmItemCommand
        {
            Type = typeId.Result
        });


        var command = new UpdateCrmItemCommand()
        {
            Id = createdEntity.Result,
            Type = typeId.Result
        };

        var id = await SendAsync(command);

        id.Result.Should().BeTrue();
    }

    [Test]
    public async Task ShouldUpdateCrmItemWithOneField()
    {
        await RunAsTestUser();

        var typeId = await SendAsync(CrmTypeTests.GetCreateTypeCommand());

        var id = await SendAsync(new CreateCrmItemCommand() { Type = typeId.Result });

        var command = new UpdateCrmItemCommand()
        {
            Id = id.Result,
            Type = typeId.Result,
            Fields = new Dictionary<string, CrmItemFieldDto>()
            {
                { "title", new CrmItemFieldDto() { Values = { "Some title" } } },
                { "not_exists_field1", new CrmItemFieldDto() { Values = { "val 1" } } },
                { "not_exists_field2", new CrmItemFieldDto() { Values = { "val 2" } } }
            }
        };

        var updateResult = await SendAsync(command);

        updateResult.Result.Should().BeTrue();

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

        var id = await SendAsync(new CreateCrmItemCommand() { Type = typeId.Result });

        var command = new UpdateCrmItemCommand()
        {
            Id = id.Result,
            Type = typeId.Result,
            Fields = new Dictionary<string, CrmItemFieldDto>()
            {
                { "title", new CrmItemFieldDto() { Values = { "Some title" } } },
                { "description", new CrmItemFieldDto() { Values = { "Some description" } } },
                { "views", new CrmItemFieldDto() { Values = { "10000" } } },
            }
        };

        var updateResult = await SendAsync(command);

        updateResult.Result.Should().BeTrue();

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
