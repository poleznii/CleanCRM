using CleanCRM.Application.Common.Exceptions;
using CleanCRM.Application.Customers.Commands.CreateCustomer;
using CleanCRM.Application.Customers.Queries.GetCustomer;

namespace CleanCRM.Application.IntegrationTests.Customers.Queries;

using static Tests;

public class GetCustomerTests : BaseTestFixture
{
    [Test]
    public async Task ShouldBeAuthorizedUser()
    {
        var command = new GetCustomerQuery();

        await FluentActions.Invoking(() => SendAsync(command)).Should().ThrowAsync<UnauthorizedAccessException>();
    }

    [Test]
    public async Task ShouldBeNotEmptyRequest()
    {
        await RunAsTestUser();

        var command = new GetCustomerQuery();

        await FluentActions.Invoking(() => SendAsync(command)).Should().ThrowAsync<ValidatorException>();
    }

    [Test]
    public async Task ShouldBeValidCustomerId()
    {
        await RunAsTestUser();

        var command = new GetCustomerQuery()
        {
            Id = -1
        };

        await FluentActions.Invoking(() => SendAsync(command)).Should().ThrowAsync<ValidatorException>();
    }

    [Test]
    public async Task ShouldBeExistingCustomerId()
    {
        await RunAsTestUser();

        var query = new GetCustomerQuery()
        {
            Id = 1111
        };

        await FluentActions.Invoking(() => SendAsync(query)).Should().ThrowAsync<NotFoundException>();
    }

    [Test]
    public async Task ShouldReturnCustomer()
    {
        await RunAsTestUser();

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
