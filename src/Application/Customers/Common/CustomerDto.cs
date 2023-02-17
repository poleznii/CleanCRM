using CleanCRM.Domain.Entities.Customers;

namespace CleanCRM.Application.Customers.Common;

public class CustomerDto
{
    public int Id { get; set; }
    public string? Name { get; set; }
    public CustomerDto(Customer entity)
    {
        Id = entity.Id;
        Name = entity.Name;
    }
}
