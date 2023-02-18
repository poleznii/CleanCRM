using CleanCRM.Application.Common.Interfaces;

namespace CleanCRM.Application.Common.Models;

public class ListResult<T> : IApiResult
{
    public IEnumerable<T> Items { get; }
    public int Total { get; }
    public long Time { get; set; }

    public ListResult(IEnumerable<T> items, int total)
    {
        Items = items;
        Total = total;
    }
}
