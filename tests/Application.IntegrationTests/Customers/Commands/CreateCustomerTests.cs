using CleanCRM.Application.Customers.Commands.CreateCustomer;
using CleanCRM.Domain.Entities.Customers;

namespace CleanCRM.Application.IntegrationTests.Customers.Commands;

public class CreateCustomerTests : BaseTestFixture
{
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
