namespace dbj_result ;

/*
 * Usage: 
 * 
 * public Result DoSomething()
{
    try
    {
        // Do some work
        return Result.Success();
    }
    catch (Exception ex)
    {
        return Result.Failure("An error occurred", ex);
    }
}

public Result<string> GetStringResult()
{
    try
    {
        // Do some work and get a string result
        string result = "Hello, World!";
        return Result<string>.Success(result);
    }
    catch (Exception ex)
    {
        return Result<string>.Failure("An error occurred", ex);
    }
}

 */

public class Result
{
    public bool IsSuccess { get; }
    public string ErrorMessage { get; }
    public Exception ? Exception { get; }

    protected Result(bool isSuccess, string errorMessage, Exception ? exception)
    {
        IsSuccess = isSuccess;
        ErrorMessage = errorMessage;
        Exception = exception;
    }

    public static Result Success()
    {
        return new Result(true, string.Empty, null);
    }

    public static Result Failure(string errorMessage, Exception ? exception = null)
    {
        return new Result(false, errorMessage, exception);
    }
}

public class Result<T> : Result
{
    public T Data { get; } 

    protected Result(bool isSuccess, T data, string errorMessage, Exception ? exception)
        : base(isSuccess, errorMessage, exception)
    {
        Data = data;
    }

    public static Result<T> Success(T data)
    {
        return new Result<T>(true, data, string.Empty, null);
    }

    public static new Result<T> Failure(string errorMessage, Exception ? exception = null)
    {
        return new Result<T>(false, default! , errorMessage, exception);
    }
}
