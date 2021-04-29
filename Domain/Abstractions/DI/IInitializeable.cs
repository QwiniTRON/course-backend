namespace course_backend.Abstractions.DI
{
    public interface IInitializeable<in TOptions>
    {
        public void InitializeOptions(TOptions options);
    }
}