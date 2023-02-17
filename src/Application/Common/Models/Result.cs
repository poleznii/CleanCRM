namespace CleanCRM.Application.Common.Models;

public class Result
{
    public bool Succeeded { get; set; }

    public Result(bool succeeded)
    {
        Succeeded = succeeded;
    }

    public static Result Success()
    {
        return new Result(true);
    }
    public static Result Fail()
    {
        return new Result(false);
    }
}
