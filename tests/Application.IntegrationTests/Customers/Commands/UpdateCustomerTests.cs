using CleanCRM.Application.Customers.Commands.CreateCustomer;
using CleanCRM.Application.Customers.Commands.EditCustomer;
using CleanCRM.Domain.Entities.Customers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanCRM.Application.IntegrationTests.Customers.Commands;

public class UpdateCustomerTests : BaseTestFixture
{
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
        entity!.Name.Should().Be(command.Name);
    }
}
