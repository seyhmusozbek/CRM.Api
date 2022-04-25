using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Concrete.UnitOfWork
{
    public class UnitOfWork<TContext> : IRepositoryFactory, IUnitOfWork<TContext>, IUnitOfWork
        where TContext : DbContext, IDisposable
    {
        private Dictionary<Type, object> _repositories;
        private IMapper _mapper;

        public UnitOfWork(TContext context,IMapper mapper)
        {
            Context = context ?? throw new ArgumentNullException(nameof(context));
            _mapper = mapper;
        }

        public IRepository<TEntity> GetRepository<TEntity>() where TEntity : class
        {
            if (_repositories == null) _repositories = new Dictionary<Type, object>();

            var type = typeof(TEntity);
            if (!_repositories.ContainsKey(type)) _repositories[type] = new Repository<TEntity>(Context,_mapper);
            return (IRepository<TEntity>) _repositories[type];
        }

        public IRepositoryAsync<TEntity> GetRepositoryAsync<TEntity>() where TEntity : class
        {
            if (_repositories == null) _repositories = new Dictionary<Type, object>();

            var type = typeof(TEntity);
            if (!_repositories.ContainsKey(type)) _repositories[type] = new RepositoryAsync<TEntity>(Context);
            return (IRepositoryAsync<TEntity>) _repositories[type];
        }

        public IRepositoryReadOnly<TEntity> GetReadOnlyRepository<TEntity>() where TEntity : class
        {
            if (_repositories == null) _repositories = new Dictionary<Type, object>();

            var type = typeof(TEntity);
            if (!_repositories.ContainsKey(type)) _repositories[type] = new RepositoryReadOnly<TEntity>(Context,_mapper);
            return (IRepositoryReadOnly<TEntity>) _repositories[type];
        }

        public TContext Context { get; }

        public int SaveChanges()
        {
            return Context.SaveChanges();
        }

        public void Dispose()
        {
            Context?.Dispose();
        }
    }
}