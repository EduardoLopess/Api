public class Result<T>
{
    public bool IsSuccess { get; }
    public string Message { get; }
    public T? Data { get; }

    private Result(bool success, string message, T? data)
    {
        IsSuccess = success;
        Message = message;
        Data = data;
    }

    public static Result<T> Success(T data, string message)
        => new(true, message, data);

    public static Result<T> Failure(string message)
        => new(false, message, default);
}