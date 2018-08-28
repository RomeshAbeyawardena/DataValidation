using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using DataValidation.Domains;

namespace DataValidation.Interfaces
{
    public interface IEntityRepository<TEntity, in TKey> where TEntity: class, IEntity<TKey> where TKey: IComparable<TKey>
    {
        bool Any(Expression<Func<TEntity, bool>> anyExpression);
        TEntity Find(TKey key);
        IQueryable<TEntity> Where(Expression<Func<TEntity, bool>> whereExpression);
        TEntity Add(TEntity newEntity);
        TEntity Update(TEntity newEntity);
        TEntity FirstOrDefault(Expression<Func<TEntity, bool>> firstOrDefaultExpression);
        void Remove(TKey key);
        IQueryable<TEntity> Include<TSource>(Expression<Func<TEntity, TSource>> selectorExpression);
        int Commit(bool saveAllChanges = true);
    }
}
