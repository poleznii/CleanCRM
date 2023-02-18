namespace CleanCRM.Application.IntegrationTests;

using static Tests;

[TestFixture]
public abstract class BaseTestFixture
{
    [SetUp]
    public async Task TestSetUp()
    {
        await ResetState();
    }
}