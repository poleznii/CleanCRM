namespace CleanCRM.Application.Common.Models;

public class ListResult<T>
{
    public IEnumerable<T> Items { get; }
    public int Total { get; }
    public ListResult(IEnumerable<T> items, int total)
    {
        Items = items;
        Total = total;
    }
}
