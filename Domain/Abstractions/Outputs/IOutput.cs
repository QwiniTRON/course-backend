namespace Domain.Abstractions.Outputs
{
    public interface IOutput
    {
        public bool Succeeded { get; }
        public object Data { get; }
        public string ErrorMessage { get; }
    }
}