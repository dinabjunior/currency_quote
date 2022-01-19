using System;

namespace Br.Com.Company.CurrencyQuote.Data.Support
{
    public sealed class Result<TResult>
    {
        private Result(bool isSuccess, TResult value)
        {
            IsSuccess = isSuccess;
            Value = value;
        }

        private Result(bool isSuccess, string errorMessage, Exception exception)
        {
            IsSuccess = isSuccess;
            ErrorMessage = errorMessage;
            Exception = exception;
        }

        public bool IsSuccess { get; private set; }
        public TResult Value { get; private set; }
        public string ErrorMessage { get; private set; }
        public Exception Exception { get; private set; }

        public static Result<T> Success<T>(T value) => new Result<T>(true, value);
        public static Result<T> Fail<T>(string errorMessage, Exception exception = null) => new Result<T>(false, errorMessage, exception);
    }
}
