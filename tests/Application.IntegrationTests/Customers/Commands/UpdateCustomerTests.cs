using CleanCRM.Application.Common.Exceptions;
using CleanCRM.Application.Customers.Commands.CreateCustomer;
using CleanCRM.Application.Customers.Commands.UpdateCustomer;
using CleanCRM.Application.Customers.Queries.GetCustomer;
using System.Text;

namespace CleanCRM.Application.IntegrationTests.Customers.Commands;

public class UpdateCustomerTests : BaseTestFixture
{
    [Test]
    public async Task ShouldBeNotEmptyRequest()
    {
        var command = new UpdateCustomerCommand();

        await FluentActions.Invoking(() => SendAsync(command)).Should().ThrowAsync<ValidatorException>();
    }

    [Test]
    public async Task ShouldBeValidCustomerId()
    {
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
        var id = await SendAsync(new CreateCustomerCommand
        {
            Name = "Customer Name"
        });

        var command = new UpdateCustomerCommand
        {
            Id = id
        };

        await FluentActions.Invoking(() => SendAsync(command)).Should().ThrowAsync<ValidatorException>();
    }

    [Test]
    public async Task ShouldBeValidNameLength()
    {
        var id = await SendAsync(new CreateCustomerCommand
        {
            Name = "Customer Name"
        });

        var command = new UpdateCustomerCommand
        {
            Id = id,
            Name = new StringBuilder(101).Insert(0, "a", 101).ToString()
        };

        await FluentActions.Invoking(() => SendAsync(command)).Should().ThrowAsync<ValidatorException>();
    }

    [Test]
    public async Task ShouldUpdateCustomer()
    {
        var id = await SendAsync(new CreateCustomerCommand
        {
            Name = "Customer Name"
        });

        var command = new UpdateCustomerCommand()
        {
            Id = id,
            Name = "Updated Name"
        };

        await SendAsync(command);

        var entity = await SendAsync(new GetCustomerQuery()
        {
            Id = id
        });

        entity.Should().NotBeNull();
        entity!.Id.Should().Be(id);
        entity!.Name.Should().Be(command.Name);
    }
}
