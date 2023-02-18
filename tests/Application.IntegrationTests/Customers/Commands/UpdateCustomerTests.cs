using CleanCRM.Application.Common.Exceptions;
using CleanCRM.Application.Customers.Commands.CreateCustomer;
using CleanCRM.Application.Customers.Commands.UpdateCustomer;
using CleanCRM.Application.Customers.Queries.GetCustomer;
using System.Text;

namespace CleanCRM.Application.IntegrationTests.Customers.Commands;

using static Tests;

public class UpdateCustomerTests : BaseTestFixture
{
    [Test]
    public async Task ShouldBeAuthorizedUser()
    {
        var command = new UpdateCustomerCommand();

        await FluentActions.Invoking(() => SendAsync(command)).Should().ThrowAsync<UnauthorizedAccessException>();
    }

    [Test]
    public async Task ShouldBeNotEmptyRequest()
    {
        await RunAsTestUser();

        var command = new UpdateCustomerCommand();

        await FluentActions.Invoking(() => SendAsync(command)).Should().ThrowAsync<ValidatorException>();
    }

    [Test]
    public async Task ShouldBeValidCustomerId()
    {
        await RunAsTestUser();

        var command = new UpdateCustomerCommand()
        {
            Id = -1,
            Name = "Customer Name"
        };

        await FluentActions.Invoking(() => SendAsync(command)).Should().ThrowAsync<ValidatorException>();
    }

    [Test]
    public async Task ShouldBeExistingCustomerId()
    {
        await RunAsTestUser();

        var command = new UpdateCustomerCommand()
        {
            Id = 1111,
            Name = "Customer Name"
        };

        await FluentActions.Invoking(() => SendAsync(command)).Should().ThrowAsync<NotFoundException>();
    }

    [Test]
    public async Task ShouldBeNotEmptyName()
    {
        await RunAsTestUser();

        var id = await SendAsync(new CreateCustomerCommand
        {
            Name = "Customer Name"
        });

        var command = new UpdateCustomerCommand
        {
            Id = id.Result
        };

        await FluentActions.Invoking(() => SendAsync(command)).Should().ThrowAsync<ValidatorException>();
    }

    [Test]
    public async Task ShouldBeValidNameLength()
    {
        await RunAsTestUser();

        var id = await SendAsync(new CreateCustomerCommand
        {
            Name = "Customer Name"
        });

        var command = new UpdateCustomerCommand
        {
            Id = id.Result,
            Name = new StringBuilder(101).Insert(0, "a", 101).ToString()
        };

        await FluentActions.Invoking(() => SendAsync(command)).Should().ThrowAsync<ValidatorException>();
    }

    [Test]
    public async Task ShouldUpdateCustomer()
    {
        await RunAsTestUser();

        var id = await SendAsync(new CreateCustomerCommand
        {
            Name = "Customer Name"
        });

        var command = new UpdateCustomerCommand()
        {
            Id = id.Result,
            Name = "Updated Name"
        };

        await SendAsync(command);

        var entity = await SendAsync(new GetCustomerQuery()
        {
            Id = id.Result
        });

        entity.Should().NotBeNull();
        entity!.Result.Id.Should().Be(id.Result);
        entity!.Result.Name.Should().Be(command.Name);
    }
}
