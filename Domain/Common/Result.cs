using System;
using System.Collections.Generic;
using System.Text;

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

        public static Result Success(string message = "")
            => new(true, message);

        public static Result Failure(string message)
            => new(false, message);
    }
}



