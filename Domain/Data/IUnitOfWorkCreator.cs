namespace Domain.Data
{
    public interface IUnitOfWorkCreator
    {
        IUnitOfWork CreateUnitOfWork();
    }
}