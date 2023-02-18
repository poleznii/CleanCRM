using CleanCRM.Application.Common.Exceptions;
using CleanCRM.Application.Customers.Commands.CreateCustomer;
using CleanCRM.Application.Customers.Queries.GetCustomerList;

namespace CleanCRM.Application.IntegrationTests.Customers.Queries;

using static Tests;

public class GetCustomerListTests : BaseTestFixture
{
    [Test]
    public async Task ShouldBeAuthorizedUser()
    {
        var command = new GetCustomerListQuery();

        await FluentActions.Invoking(() => SendAsync(command)).Should().ThrowAsync<UnauthorizedAccessException>();
    }

    [Test]
    public async Task ShouldBeValidListParams()
    {
        await RunAsTestUser();

        var query = new GetCustomerListQuery()
        {
            Skip = -1,
            Take = -1
        };

        await FluentActions.Invoking(() => SendAsync(query)).Should().ThrowAsync<ValidatorException>();
    }

    [Test]
    public async Task ShouldReturnEmptyList()
    {
        await RunAsTestUser();

        var query = new GetCustomerListQuery();

        var listResult = await SendAsync(query);

        listResult.Should().NotBeNull();
        listResult!.Items.Should().HaveCount(0);
        listResult!.Total.Should().Be(0);
    }

    [Test]
    public async Task ShouldReturnList()
    {
        await RunAsTestUser();

        var commands = new List<CreateCustomerCommand>();
        for (var i = 0;i < 10; i++)
        {
            commands.Add(new CreateCustomerCommand()
            {
                Name = $"Customer {i}",
            });
        }

        foreach (var command in commands)
        {
            await SendAsync(command);
        }

        var query = new GetCustomerListQuery();

        var listResult = await SendAsync(query);

        listResult.Should().NotBeNull();
        listResult!.Items.Should().HaveCount(commands.Count);
        listResult!.Total.Should().Be(commands.Count);
    }

    [Test]
    public async Task ShouldReturnPartOfList()
    {
        await RunAsTestUser();

        var commands = new List<CreateCustomerCommand>();
        for (var i = 0; i < 10; i++)
        {
            commands.Add(new CreateCustomerCommand()
            {
                Name = $"Customer {i}",
            });
        }

        foreach (var command in commands)
        {
            await SendAsync(command);
        }

        var query = new GetCustomerListQuery()
        {
            Skip = 0,
            Take = commands.Count - 1
        };

        var listResult = await SendAsync(query);

        listResult.Should().NotBeNull();
        listResult!.Items.Should().HaveCount(commands.Count - 1);
        listResult!.Total.Should().Be(commands.Count);
    }
}
