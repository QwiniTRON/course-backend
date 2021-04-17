namespace course_backend.Services
{
    public class ValidateResult
    {
        public bool Succeeded { get; set; }
        public string ErrorMessage { get; set; }

        public ValidateResult(bool succeeded, string errorMessage = null)
        {
            Succeeded = succeeded;
            ErrorMessage = errorMessage;
        }

        public static ValidateResult Error(string message) => new ValidateResult(false, message);
        public static ValidateResult Success() => new ValidateResult(true);
    }
}