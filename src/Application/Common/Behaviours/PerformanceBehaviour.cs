using CleanCRM.Application.Common.Interfaces;
using MediatR;
using System.Diagnostics;

namespace CleanCRM.Application.Common.Behaviours;

public class PerformanceBehaviour<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse> where TRequest : notnull, IApiRequest
{
    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        var timer = Stopwatch.StartNew();

        var response = await next();

        timer.Stop();

        var elapsedMilliseconds = timer.ElapsedMilliseconds;

        if (response is IApiResult apiResult)
        {
            apiResult.Time = elapsedMilliseconds;
        }

        if (elapsedMilliseconds > 1000)
        {
            //TODO log?
        }

        return response;
    }
}
