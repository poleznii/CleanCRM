namespace CleanCRM.Application.Common.Models;

public class Result
{
    public string[] Errors { get; set; }
    public bool Succeeded { get; set; }

    public Result(bool succeeded, IEnumerable<string> errors)
    {
        Succeeded = succeeded;
        Errors = errors.ToArray();
    }

    public string FailureMessage => Errors?.FirstOrDefault() ?? string.Empty;

    public static Result Success()
    {
        return new Result(true, Array.Empty<string>());
    }
    public static Result Fail(IEnumerable<string> errors)
    {
        return new Result(false, errors);
    }
    public static Result Fail(string error)
    {
        return new Result(false, new[] { error });
    }
}
