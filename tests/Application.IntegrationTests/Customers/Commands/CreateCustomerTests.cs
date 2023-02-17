using CleanCRM.Application.Common.Exceptions;
using CleanCRM.Application.Customers.Commands.CreateCustomer;
using CleanCRM.Domain.Entities.Customers;
using System.Text;

namespace CleanCRM.Application.IntegrationTests.Customers.Commands;

public class CreateCustomerTests : BaseTestFixture
{
    [Test]
    public async Task ShouldBeNotEmptyRequest()
    {
        var command = new CreateCustomerCommand();

        await FluentActions.Invoking(() => SendAsync(command)).Should().ThrowAsync<ValidatorException>();
    }

    [Test]
    public async Task ShouldBeValidNameLength()
    {
        var command = new CreateCustomerCommand
        {
            Name = new StringBuilder(101).Insert(0, "a", 101).ToString()
        };

        await FluentActions.Invoking(() => SendAsync(command)).Should().ThrowAsync<ValidatorException>();
    }

    [Test]
    public async Task ShouldCreateCustomer()
    {
        var command = new CreateCustomerCommand
        {
            Name = "Customer name"
        };

        var id = await SendAsync(command);

        var entity = await FindAsync<Customer>(id);

        entity.Should().NotBeNull();
        entity!.Name.Should().Be(command.Name);
    }
}
