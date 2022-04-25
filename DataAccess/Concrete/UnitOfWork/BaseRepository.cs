using System.Linq.Expressions;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using DataAccess.Concrete.UnitOfWork.Paging;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;

namespace DataAccess.Concrete.UnitOfWork
{
    public abstract class BaseRepository<T> : IReadRepository<T> where T : class
    {
        protected readonly DbContext _dbContext;
        protected readonly DbSet<T> _dbSet;
        protected readonly IMapper _mapper;

        public BaseRepository(DbContext context ,IMapper mapper)
        {
            _dbContext = context ?? throw new ArgumentException(nameof(context));
            _dbSet = _dbContext.Set<T>();
            _mapper = mapper;
        }

        public virtual IQueryable<T> Query(string sql, params object[] parameters) => _dbSet.FromSqlRaw(sql, parameters);

        public T Search(params object[] keyValues) => _dbSet.Find(keyValues);
       

        public T Single(Expression<Func<T, bool>> predicate = null,
            Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
            Func<IQueryable<T>, IIncludableQueryable<T, object>> include = null,
            bool disableTracking = false)
        {
            IQueryable<T> query = _dbSet;
            if (disableTracking) query = query.AsNoTracking();

            if (include != null) query = include(query);

            if (predicate != null) query = query.Where(predicate);

            if (orderBy != null)
                return orderBy(query).FirstOrDefault();
            return query.FirstOrDefault();
        }

        public TDestination Single<TDestination>(Expression<Func<T, bool>> predicate = null, 
            Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null, Func<IQueryable<T>, 
                IIncludableQueryable<T, object>> include = null, 
            bool disableTracking = false)
        {
            IQueryable<T> query = _dbSet;
            if (disableTracking) query = query.AsNoTracking();

            if (include != null) query = include(query);

            if (predicate != null) query = query.Where(predicate);

            if (orderBy != null)
                return orderBy(query).AsQueryable().ProjectTo<TDestination>(_mapper.ConfigurationProvider).FirstOrDefault();
            var returned = query.ProjectTo<TDestination>(_mapper.ConfigurationProvider);
            return returned.FirstOrDefault();
        }

        public IPaginate<T> GetList(Expression<Func<T, bool>> predicate = null,
            Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
            Func<IQueryable<T>, IIncludableQueryable<T, object>> include = null, int index = 0,
            int size = 20, bool disableTracking = false)
        {
            IQueryable<T> query = _dbSet;
            if (disableTracking) query = query.AsNoTracking();

            if (include != null) query = include(query);

            if (predicate != null) query = query.Where(predicate);

            return orderBy != null ? orderBy(query).ToPaginate(index, size) : query.ToPaginate(index, size);
        }


        public IPaginate<TResult> GetList<TResult>(Expression<Func<T, TResult>> selector,
            Expression<Func<T, bool>> predicate = null, Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
            Func<IQueryable<T>, IIncludableQueryable<T, object>> include = null,
            int index = 0, int size = 20, bool disableTracking = false) where TResult : class
        {
            IQueryable<T> query = _dbSet;
            if (disableTracking) query = query.AsNoTracking();

            if (include != null) query = include(query);

            if (predicate != null) query = query.Where(predicate);

            return orderBy != null
                ? orderBy(query).Select(selector).ToPaginate(index, size)
                : query.Select(selector).ToPaginate(index, size);
        }
    }
}