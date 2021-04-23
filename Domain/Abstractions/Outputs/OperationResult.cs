using System;
using System.ComponentModel.DataAnnotations;

namespace Domain.Abstractions.Outputs
{
    public class OperationResult<TData>: IOperationResult<TData>
    {
        public bool Succeeded { get; }
        public TData Data { get; }
        public string ErrorMessage { get; }
        public Exception Error { get; set; }

        /* named ctors */
        public static OperationResult<object> Success 
            => new OperationResult<object>(true);
        public static OperationResult<TData> SuccessData(TData data) 
            => new OperationResult<TData>(true, data);
        public static OperationResult<TData> Failure(string errorMessage) 
            => new OperationResult<TData>(false, default, errorMessage);
        public static OperationResult<TData> ErrorData(string errorMessage, Exception error) => 
            new OperationResult<TData>(false, default, errorMessage).HasError(error);
        
        /* ctor */
        public OperationResult(bool succeeded, TData data = default, string errorMessage = null, Exception error = null)
        {
            Succeeded = succeeded;
            Data = data;
            ErrorMessage = errorMessage;
            Error = error;
        }

        /* setters */
        public OperationResult<TData> HasError(Exception error)
        {
            Error = error;
            return this;
        }
    }
}