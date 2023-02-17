using CleanCRM.Application.Customers.Commands.CreateCustomer;
using CleanCRM.Application.Customers.Queries.GetCustomerList;

namespace CleanCRM.Application.IntegrationTests.Customers.Queries;

public class GetCustomerListTests : BaseTestFixture
{
    [Test]
    public async Task ShouldReturnEmptyList()
    {
        var query = new GetCustomerListQuery();

        var listResult = await SendAsync(query);

        listResult.Should().NotBeNull();
        listResult!.Items.Should().HaveCount(0);
        listResult!.Total.Should().Be(0);
    }

    [Test]
    public async Task ShouldReturnList()
    {
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
