namespace CleanCRM.Application.Customers.Common;

public class CustomerDto
{
    public int Id { get; set; }
    public string Name { get; set; }
    public CustomerDto(int id, string name)
    {
        Id = id;
        Name = name;
    }
}
