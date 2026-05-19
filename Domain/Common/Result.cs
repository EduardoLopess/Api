using System;

namespace Domain.Common
{
    
    public sealed record Result
    {
        public bool IsSuccess { get; }
        public string Message { get; }
        public bool IsFailure => !IsSuccess;

        private Result(bool success, string message)
        {
            IsSuccess = success;
            Message = message;
        }

        public static Result Success(string message = "") => new(true, message);
        public static Result Failure(string message) => new(false, message);
    }


    public sealed record Result<T>
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


        public static Result<T> Success(T value, string message = "")
            => new(true, message, value);


        public static Result<T> Failure(string message)
            => new(false, message, default);
    }
}