using System;
using System.Collections.Generic;
using System.Text;

namespace BloodBank.Logics
{
    public class Result
    {
        public bool IsScuccessful { get; set; }
        public IEnumerable<ErrorMessage> ErrorMessages = new List<ErrorMessage>();

        public static Result<T>Ok<T>(T value)
        {
            return new Result<T>
            {
                IsScuccessful = true,
                Value = value
            };
        }

        public static Result<T> Error<T>(string message)
        {
            return new Result<T>
            {
                IsScuccessful = false,
                ErrorMessages = new List<ErrorMessage>
                {
                    new ErrorMessage
                    {
                        PropertyName=string.Empty,
                        Message=message
                    }
                }
            };
        }

        
    }

    public class Result<T> : Result
    {
        public T Value { get; set; }
    }

    public class ErrorMessage
    {
        public string PropertyName { get; set; }
        public string Message { get; set; }
    }
}
