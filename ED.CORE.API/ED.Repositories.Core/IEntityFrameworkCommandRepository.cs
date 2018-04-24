using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace ED.Repositories.Core
{
    public interface IEntityFrameworkCommandRepository<TEntity> where TEntity : class
    {
        void Add(TEntity entity);
        void AddRange(IEnumerable<TEntity> entities);
        void Update(TEntity entity);
        void Update(Expression<Func<TEntity, TEntity>> updateExpression);
        void Update(Expression<Func<TEntity, bool>> filterExpression, Expression<Func<TEntity, TEntity>> updateExpression);
        void Delete(TEntity entity);
        void Delete(ICollection<TEntity> entityCollection);
        int Delete(Expression<Func<TEntity, bool>> filterExpression);
        void Commit();
    }
}