using CleanCRM.Application.Common.Interfaces;

namespace CleanCRM.Application.Common.Models;

public class ItemResult<T> : IApiResult
{
    public required T Result { get; init; } = default!;
    public long Time { get; set; }
}
