using CleanCRM.Application.Common.Exceptions;
using CleanCRM.Application.Customers.Commands.CreateCustomer;
using CleanCRM.Application.Customers.Commands.DeleteCustomer;

namespace CleanCRM.Application.IntegrationTests.Customers.Commands;

public class DeleteCustomerTests : BaseTestFixture
{
    [Test]
    public async Task ShouldBeNotEmptyRequest()
    {
        var command = new DeleteCustomerCommand();

        await FluentActions.Invoking(() => SendAsync(command)).Should().ThrowAsync<ValidatorException>();
    }

    [Test]
    public async Task ShouldBeValidCustomerId()
    {
        var command = new DeleteCustomerCommand()
        {
            Id = -1
        };

        await FluentActions.Invoking(() => SendAsync(command)).Should().ThrowAsync<ValidatorException>();
    }

    [Test]
    public async Task ShouldBeExistingCustomerId()
    {
        var command = new DeleteCustomerCommand()
        {
            Id = 1111
        };

        await FluentActions.Invoking(() => SendAsync(command)).Should().ThrowAsync<NotFoundException>();
    }

    [Test]
    public async Task ShouldDeleteCustomer()
    {
        var id = await SendAsync(new CreateCustomerCommand
        {
            Name = "Customer name"
        });

        var command = new DeleteCustomerCommand()
        {
            Id = id
        };

        var result = await SendAsync(command);

        result.Should().NotBeNull();
        result!.Succeeded.Should().BeTrue();
    }
}
