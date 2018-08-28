using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using DataValidation.Domains;
using DataValidation.Interfaces;

namespace DataValidation.DataLayer
{
    public class ListEntityRepository<TCollection, TEntity, TKey> : IEntityRepository<TEntity, TKey> 
        where TKey : IComparable<TKey> 
        where TEntity : class, IEntity<TKey>
        where TCollection: ICollection<TEntity>
    {
        public ListEntityRepository(IInMemoryList<TCollection, TEntity, TKey> inMemoryList)
        {
            _inMemoryList = inMemoryList;
        }

        public bool Any(Expression<Func<TEntity, bool>> anyExpression)
        {
            return _inMemoryList.Any(anyExpression);
        }

        public TEntity Find(TKey key)
        {
            return FirstOrDefault(entity => entity.Id.Equals(key));
        }

        public IQueryable<TEntity> Where(Expression<Func<TEntity, bool>> whereExpression)
        {
            return _inMemoryList.Where(whereExpression).AsQueryable();
        }

        public TEntity Add(TEntity newEntity)
        {
            _inMemoryList.Add(newEntity);
            return newEntity;
        }

        public TEntity Update(TEntity updatedEntity)
        {
            _inMemoryList.Update(updatedEntity);
            return updatedEntity;
        }

        public TEntity FirstOrDefault(Expression<Func<TEntity, bool>> firstOrDefaultExpression)
        {
            return _inMemoryList.FirstOrDefault(firstOrDefaultExpression);
        }

        public IQueryable<TEntity> FullTextWhere(Expression<Func<TEntity, bool>> whereExpression, string freeTextExpression,
            string fullTextSearchCriteria)
        {
            throw new NotImplementedException();
        }

        public void Remove(TKey key)
        {
            var foundEntry = Find(key);

            _inMemoryList.Remove(foundEntry);
        }

        public IQueryable<TEntity> Include<TSource>(Expression<Func<TEntity, TSource>> selectorExpression)
        {
            throw new NotImplementedException();
        }

        public int Commit(bool saveAllChanges = true)
        {
            return _inMemoryList.Commit();
        }

      
        private readonly IInMemoryList<TCollection, TEntity, TKey> _inMemoryList;
    }
}