public class Result<T>
{
    public bool IsSuccess { get; }
    public string Message { get; }
    public bool IsFailure => !IsSuccess;
    public T? Value { get; }

    private Result(bool success, string message, T? value)
    {
        IsSuccess = success;
        Message = message;
        Value = value;
    }

    public static Result<T> Success(T value, string message)
        => new(true, message, value);

    public static Result<T> Failure(string message)
        => new(false, message, default);
}