using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Concrete.UnitOfWork
{
    public class RepositoryReadOnly<T> : BaseRepository<T>, IRepositoryReadOnly<T> where T : class
    {
        public RepositoryReadOnly(DbContext context,IMapper mapper) : base(context,mapper)
        {
        }
    }
}