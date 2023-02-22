using CleanCRM.Application.Common.Exceptions;
using CleanCRM.Application.CrmTypes.Commands.CreateCrmType;
using CleanCRM.Application.CrmTypes.Common;
using CleanCRM.Application.CrmTypes.Queries.GetCrmType;
using CleanCRM.Domain.ValueObjects;
using System.Text;

namespace CleanCRM.Application.IntegrationTests.CrmTypes.Commands;

using static Tests;

public class CreateCrmTypeTests : BaseTestFixture
{
    [Test]
    public async Task ShouldBeAuthorizedUser()
    {
        var command = new CreateCrmTypeCommand();

        await FluentActions.Invoking(() => SendAsync(command)).Should().ThrowAsync<UnauthorizedAccessException>();
    }

    [Test]
    public async Task ShouldBeNotEmptyRequest()
    {
        await RunAsTestUser();

        var command = new CreateCrmTypeCommand();

        await FluentActions.Invoking(() => SendAsync(command)).Should().ThrowAsync<ValidatorException>();
    }

    [Test]
    public async Task ShouldBeValidIdLength()
    {
        await RunAsTestUser();

        var command = new CreateCrmTypeCommand
        {
            Id = new StringBuilder(101).Insert(0, "a", 101).ToString()
        };

        await FluentActions.Invoking(() => SendAsync(command)).Should().ThrowAsync<ValidatorException>();
    }

    [Test]
    public async Task ShouldNotCreateDuplicate()
    {
        await RunAsTestUser();

        var command = new CreateCrmTypeCommand
        {
            Id = "customer"
        };

        var id = await SendAsync(command);

        await FluentActions.Invoking(() => SendAsync(command)).Should().ThrowAsync<ValidatorException>();
    }

    [Test]
    public async Task ShouldCreateCrmTypeWithNoFields()
    {
        await RunAsTestUser();

        var command = new CreateCrmTypeCommand
        {
            Id = "customer"
        };

        var id = await SendAsync(command);

        var entity = await SendAsync(new GetCrmTypeQuery()
        {
            Id = id.Result
        });

        entity.Result.Should().NotBeNull();
        entity.Result!.Id.Should().Be(command.Id);
        entity.Result!.Fields.Should().HaveCount(0);
    }

    [Test]
    public async Task ShouldCreateCrmTypeWithOneField()
    {
        await RunAsTestUser();

        var command = new CreateCrmTypeCommand
        {
            Id = "customer",
            Fields = new Dictionary<string, CrmTypeFieldPropertiesDto>()
            {
                {
                    "title", new CrmTypeFieldPropertiesDto() {
                        Type = CrmFieldType.String
                    }
                }
            }
        };

        var id = await SendAsync(command);

        var entity = await SendAsync(new GetCrmTypeQuery()
        {
            Id = id.Result
        });

        entity.Result.Should().NotBeNull();
        entity.Result!.Id.Should().Be(command.Id);
        entity.Result!.Fields.Should().HaveCount(1);
        entity.Result!.Fields["title"].Should().NotBeNull();
        entity.Result!.Fields["title"].Metadata["type"].Should().NotBeNull();
        entity.Result!.Fields["title"].Metadata["type"].Should().Be(CrmFieldType.String.ToString());
    }


    [Test]
    public async Task ShouldCreateCrmTypeWithTwoFields()
    {
        await RunAsTestUser();

        var command = new CreateCrmTypeCommand
        {
            Id = "customer",
            Fields = new Dictionary<string, CrmTypeFieldPropertiesDto>()
            {
                {
                    "title", new CrmTypeFieldPropertiesDto() {
                        Type = CrmFieldType.String
                    }
                },
                {
                    "views", new CrmTypeFieldPropertiesDto() {
                        Type = CrmFieldType.Integer
                    }
                }
            }
        };

        var id = await SendAsync(command);

        var entity = await SendAsync(new GetCrmTypeQuery()
        {
            Id = id.Result
        });

        entity.Result.Should().NotBeNull();
        entity.Result!.Id.Should().Be(command.Id);
        entity.Result!.Fields.Should().HaveCount(2);
        entity.Result!.Fields["title"].Should().NotBeNull();
        entity.Result!.Fields["title"].Metadata["type"].Should().NotBeNull();
        entity.Result!.Fields["title"].Metadata["type"].Should().Be(CrmFieldType.String.ToString());
        entity.Result!.Fields["views"].Should().NotBeNull();
        entity.Result!.Fields["views"].Metadata["type"].Should().NotBeNull();
        entity.Result!.Fields["views"].Metadata["type"].Should().Be(CrmFieldType.Integer.ToString());
    }

}
