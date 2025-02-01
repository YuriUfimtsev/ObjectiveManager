using Newtonsoft.Json;

namespace ObjectiveManager.Models.Result;

public class Result
{
    public bool Succeeded { get; }
    public string[] Errors { get; }

    [JsonConstructor]
    private Result(bool succeeded, string[] errors)
    {
        Succeeded = succeeded;
        Errors = errors;
    }

    public static Result Success()
    {
        return new Result(true, null);
    }

    public static Result Failed(params string[] errors)
    {
        return new Result(false, errors);
    }
}