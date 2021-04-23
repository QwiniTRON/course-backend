using System;

namespace Domain.Abstractions.Outputs
{
    public interface IOperationResult<out TData>
    {
        public bool Succeeded { get; }
        public TData Data { get; }
        public string ErrorMessage { get; }
        public Exception Error { get; }
    }
}