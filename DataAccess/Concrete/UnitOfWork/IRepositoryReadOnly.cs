namespace DataAccess.Concrete.UnitOfWork
{
    public interface IRepositoryReadOnly<T> : IReadRepository<T> where T : class
    {
       
    }
}