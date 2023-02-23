using CleanCRM.Application.Common.Exceptions;
using CleanCRM.Application.CrmTypes.Commands.CreateCrmType;
using CleanCRM.Application.CrmTypes.Commands.UpdateCrmType;
using CleanCRM.Application.CrmTypes.Common;
using CleanCRM.Application.CrmTypes.Queries.GetCrmType;
using CleanCRM.Domain.ValueObjects;

namespace CleanCRM.Application.IntegrationTests.CrmTypes.Commands;

using static Tests;

internal class UpdateCrmTypeTests : BaseTestFixture
{
    [Test]
    public async Task ShouldBeAuthorizedUser()
    {
        var command = new UpdateCrmTypeCommand();

        await FluentActions.Invoking(() => SendAsync(command)).Should().ThrowAsync<UnauthorizedAccessException>();
    }

    [Test]
    public async Task ShouldBeNotEmptyRequest()
    {
        await RunAsTestUser();

        var command = new UpdateCrmTypeCommand();

        await FluentActions.Invoking(() => SendAsync(command)).Should().ThrowAsync<ValidatorException>();
    }



    [Test]
    public async Task ShouldBeExistingCrmType()
    {
        await RunAsTestUser();

        var command = new UpdateCrmTypeCommand
        {
            Id = "customer"
        };

        await FluentActions.Invoking(() => SendAsync(command)).Should().ThrowAsync<ValidatorException>();
    }

    [Test]
    public async Task ShouldUpdateCrmTypeWithNoFields()
    {
        await RunAsTestUser();

        var id = await SendAsync(new CreateCrmTypeCommand
        {
            Id = "customer"
        });

        var command = new UpdateCrmTypeCommand()
        {
            Id = id.Result,
            Fields = new Dictionary<string, CrmTypeFieldPropertiesDto>()
            {
                { "title", new CrmTypeFieldPropertiesDto() { Type = CrmFieldType.String } }
            }
        };

        await SendAsync(command);

        var entity = await SendAsync(new GetCrmTypeQuery()
        {
            Id = id.Result
        });

        entity.Result.Should().NotBeNull();
        entity.Result!.Id.Should().Be(command.Id);
        entity.Result!.Fields.Should().HaveCount(1);
    }

    [Test]
    public async Task ShouldUpdateCrmTypeAndReplaceFields()
    {
        await RunAsTestUser();

        var id = await SendAsync(new CreateCrmTypeCommand
        {
            Id = "customer",
            Fields = new Dictionary<string, CrmTypeFieldPropertiesDto>()
            {
                { "title1", new CrmTypeFieldPropertiesDto() { Type = CrmFieldType.String } },
                { "title2", new CrmTypeFieldPropertiesDto() { Type = CrmFieldType.String } },
                { "title3", new CrmTypeFieldPropertiesDto() { Type = CrmFieldType.String } }
            }
        });

        var command = new UpdateCrmTypeCommand()
        {
            Id = id.Result,
            Fields = new Dictionary<string, CrmTypeFieldPropertiesDto>()
            {
                { "title", new CrmTypeFieldPropertiesDto() { Type = CrmFieldType.Integer } }
            }
        };

        await SendAsync(command);

        var entity = await SendAsync(new GetCrmTypeQuery()
        {
            Id = id.Result
        });

        entity.Result.Should().NotBeNull();
        entity.Result!.Id.Should().Be(command.Id);
        entity.Result!.Fields.Should().HaveCount(1);
        entity.Result!.Fields["title"].Should().NotBeNull();
        entity.Result!.Fields["title"].Metadata["type"].Should().NotBeNull();
        entity.Result!.Fields["title"].Metadata["type"].Should().Be(CrmFieldType.Integer.ToString());
    }

}
