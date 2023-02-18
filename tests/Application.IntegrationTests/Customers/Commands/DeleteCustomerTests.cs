using CleanCRM.Application.Common.Exceptions;
using CleanCRM.Application.Customers.Commands.CreateCustomer;
using CleanCRM.Application.Customers.Commands.DeleteCustomer;

namespace CleanCRM.Application.IntegrationTests.Customers.Commands;

using static Tests;

public class DeleteCustomerTests : BaseTestFixture
{
    [Test]
    public async Task ShouldBeAuthorizedUser()
    {
        var command = new DeleteCustomerCommand();

        await FluentActions.Invoking(() => SendAsync(command)).Should().ThrowAsync<UnauthorizedAccessException>();
    }

    [Test]
    public async Task ShouldBeNotEmptyRequest()
    {
        await RunAsTestUser();

        var command = new DeleteCustomerCommand();

        await FluentActions.Invoking(() => SendAsync(command)).Should().ThrowAsync<ValidatorException>();
    }

    [Test]
    public async Task ShouldBeValidCustomerId()
    {
        await RunAsTestUser();

        var command = new DeleteCustomerCommand()
        {
            Id = -1
        };

        await FluentActions.Invoking(() => SendAsync(command)).Should().ThrowAsync<ValidatorException>();
    }

    [Test]
    public async Task ShouldBeExistingCustomerId()
    {
        await RunAsTestUser();

        var command = new DeleteCustomerCommand()
        {
            Id = 1111
        };

        await FluentActions.Invoking(() => SendAsync(command)).Should().ThrowAsync<NotFoundException>();
    }

    [Test]
    public async Task ShouldDeleteCustomer()
    {
        await RunAsTestUser();

        var id = await SendAsync(new CreateCustomerCommand
        {
            Name = "Customer name"
        });

        var command = new DeleteCustomerCommand()
        {
            Id = id.Result
        };

        var result = await SendAsync(command);

        result.Should().NotBeNull();
        result!.Result.Should().BeTrue();
    }
}
