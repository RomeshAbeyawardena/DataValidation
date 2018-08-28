using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using DataValidation.Domains;

namespace DataValidation.Interfaces
{
    public interface IInMemoryList<TCollection, TEntity, TKey> 
        where TCollection: ICollection<TEntity>
        where TEntity : IEntity<TKey> 
        where TKey : IComparable<TKey>
    {
        InMemoryListItem<TEntity, TKey> this[string key] { get; }
        TEntity Add(TEntity newEntity);
        TEntity Update(TEntity updatedEntity);
        void Remove(TEntity entity);
        int Commit();
        
        bool Any(Expression<Func<TEntity, bool>> whereExpression);
        TEntity Find(Expression<Func<TEntity, bool>> whereExpression);
        IEnumerable<TEntity> Where(Expression<Func<TEntity, bool>> whereExpression);
        TEntity FirstOrDefault(Expression<Func<TEntity, bool>> whereExpression);
        
        IList<InMemoryListItem<TEntity, TKey>> InMemoryListItems { get; }
    }
}