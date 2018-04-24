using System;
using System.Data.SqlClient;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using Dapper.LambdaExtension.Extentions;
using ED.Common;
using ED.Repositories.Core;

namespace ED.Repositories.Dapper
{
    public class DapperRepositoryBase<TEntity> : IDapperQueryRepository<TEntity>
        where TEntity : class
    {
        private readonly ThreadLocal<DapperContext> _localCtx = new ThreadLocal<DapperContext>(() => new DapperContext(Global.CommandDB));

        protected DapperContext DbContext => _localCtx.Value;

        public TEntity FindSingle(Expression<Func<TEntity, bool>> exp = null)
        {
            using (var db = DbContext.GetConnection())
            {
                return db.QueryFirstOrDefault(exp);
            }
        }

        public IQueryable<TEntity> Find(Expression<Func<TEntity, bool>> exp = null)
        {
            return Filter(exp);
        }
    
        public IQueryable<TEntity> Find(Expression<Func<TEntity, bool>> expression, Expression<Func<TEntity, dynamic>> sortPredicate, SortOrder sortOrder, int pageNumber, int pageSize)
        {
            if (pageNumber <= 0)
                throw new ArgumentOutOfRangeException(nameof(pageNumber), pageNumber, "pageNumber must great than or equal to 1.");
            if (pageSize <= 0)
                throw new ArgumentOutOfRangeException(nameof(pageSize), pageSize, "pageSize must great than or equal to 1.");
            using (var db = DbContext.GetConnection())
            {
                var query = db.Query<TEntity>().AsQueryable().Where(expression);
                var skip = (pageNumber - 1) * pageSize;
                var take = pageSize;
                if (sortPredicate == null)
                    throw new InvalidOperationException("Based on the paging query must specify sorting fields and sort order.");

                switch (sortOrder)
                {
                    case SortOrder.Ascending:
                        var pagedAscending = query.SortBy(sortPredicate).Skip(skip).Take(take);

                        return pagedAscending;
                    case SortOrder.Descending:
                        var pagedDescending = query.SortByDescending(sortPredicate).Skip(skip).Take(take);
                        return pagedDescending;
                }

                throw new InvalidOperationException("Based on the paging query must specify sorting fields and sort order.");
            }
            
        }

        private IQueryable<TEntity> Filter(Expression<Func<TEntity, bool>> exp)
        {
            using (var db = DbContext.GetConnection())
            {
                var dbSet = db.Query<TEntity>().AsQueryable();
                if (exp != null)
                    dbSet = dbSet.Where(exp);
                return dbSet;
            }
        }

        public int GetCount(Expression<Func<TEntity, bool>> exp = null)
        {
            return Filter(exp).Count();
        }
    }
}
