using CleanCRM.Application.Common.Exceptions;
using CleanCRM.Application.Customers.Commands.CreateCustomer;
using CleanCRM.Application.Customers.Commands.EditCustomer;
using CleanCRM.Domain.Entities.Customers;

namespace CleanCRM.Application.IntegrationTests.Customers.Commands;

public class UpdateCustomerTests : BaseTestFixture
{
    [Test]
    public async Task ShouldBeExistingCustomerId()
    {
        var command = new UpdateCustomerCommand()
        {
            Id = 1111
        };

        await FluentActions.Invoking(() => SendAsync(command)).Should().ThrowAsync<NotFoundException>();
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

        var entity = await FindAsync<Customer>(id);

        entity.Should().NotBeNull();
        entity!.Id.Should().Be(id);
        entity!.Name.Should().Be(command.Name);
    }
}
