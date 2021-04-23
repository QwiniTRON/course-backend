namespace Domain.Abstractions.Outputs
{
    /* general answer from api */
    public class ActionOutput: IOutput
    {
        public bool Succeeded { get; }
        public object Data { get; }
        public string ErrorMessage { get; }

        public static ActionOutput Success => new ActionOutput(true);
        public static ActionOutput SuccessData(object data) => new ActionOutput(true, data);
        public static ActionOutput Error(string errorMessage) => new ActionOutput(false, null, errorMessage);
        public static ActionOutput ErrorData(string errorMessage, object data) => new ActionOutput(false, data, errorMessage);
        
        public ActionOutput(bool succeeded, object data = null, string errorMessage = null)
        {
            Succeeded = succeeded;
            Data = data;
            ErrorMessage = errorMessage;
        }
    }
}