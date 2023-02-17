using CleanCRM.Application.Common.Exceptions;
using CleanCRM.Application.Customers.Commands.CreateCustomer;
using CleanCRM.Application.Customers.Queries.GetCustomer;

namespace CleanCRM.Application.IntegrationTests.Customers.Queries;

public class GetCustomerTests : BaseTestFixture
{
    [Test]
    public async Task ShouldBeNotEmptyRequest()
    {
        var command = new GetCustomerQuery();

        await FluentActions.Invoking(() => SendAsync(command)).Should().ThrowAsync<ValidatorException>();
    }

    [Test]
    public async Task ShouldBeValidCustomerId()
    {
        var command = new GetCustomerQuery()
        {
            Id = -1
        };

        await FluentActions.Invoking(() => SendAsync(command)).Should().ThrowAsync<ValidatorException>();
    }

    [Test]
    public async Task ShouldBeExistingCustomerId()
    {
        var query = new GetCustomerQuery()
        {
            Id = 1111
        };

        await FluentActions.Invoking(() => SendAsync(query)).Should().ThrowAsync<NotFoundException>();
    }

    [Test]
    public async Task ShouldReturnCustomer()
    {
        var command = new CreateCustomerCommand
        {
            Name = "Customer name"
        };
        var id = await SendAsync(command);

        var query = new GetCustomerQuery()
        {
            Id = id
        };

        var entity = await SendAsync(query);

        entity.Should().NotBeNull();
        entity!.Id.Should().Be(id);
        entity!.Name.Should().Be(command.Name);
    }
}
