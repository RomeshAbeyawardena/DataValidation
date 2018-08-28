using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using DataValidation.Domains;
using DataValidation.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace DataValidation.DataLayer
{
    public abstract class SqlEntityRepository<TDbContext, TEntity, TKey> : IEntityRepository<TEntity, TKey>
        where TDbContext : DbContext
        where TEntity : class, IEntity<TKey>
        where TKey : IComparable<TKey>
    {
        public bool Any(Expression<Func<TEntity, bool>> anyExpression)
        {
            return DbSet.Any(anyExpression);
        }

        public TEntity Find(TKey key)
        {
            return DbSet.Find(key);
        }

        public IQueryable<TEntity> Where(Expression<Func<TEntity, bool>> whereExpression)
        {
            return DbSet.Where(whereExpression);
        }


        public virtual TEntity Add(TEntity newEntity)
        {
            DbSet.Add(newEntity);
            return newEntity;
        }

        public virtual TEntity Update(TEntity newEntity)
        {
            DbSet.Update(newEntity);
            return newEntity;
        }

        public TEntity FirstOrDefault(Expression<Func<TEntity, bool>> firstOrDefaultExpression)
        {
            return DbSet.FirstOrDefault(firstOrDefaultExpression);
        }

        public IQueryable<TEntity> FullTextWhere(Expression<Func<TEntity, bool>> whereExpression, string fullTextExpression,
            string fullTextSearchCriteria)
        {
            return Where(a =>
                EF.Functions.FreeText(fullTextExpression, fullTextSearchCriteria))
                .Where(whereExpression);
        }

        public void Remove(TKey key)
        {
            if (key == null)
                throw new ArgumentNullException(nameof(key));

            var foundEntity = Find(key);

            if (foundEntity == null)
                throw new ArgumentException($"Unable to find key: {key}", nameof(key));

            DbSet.Remove(foundEntity);
        }

        public IQueryable<TEntity> Include<TSource>(Expression<Func<TEntity, TSource>> selectorExpression)
        {
            return DbSet.Include(selectorExpression);
        }

        public int Commit(bool saveAllChanges = true)
        {
            return Context.SaveChanges(saveAllChanges);
        }

        protected SqlEntityRepository(TDbContext context)
        {
            Context = context;
            DbSet = context.Set<TEntity>();
        }

        protected IQueryable<TEntity> IncludeMany<TSelector>(
            params Expression<Func<TEntity, TSelector>>[] includeExpression)
        {
            IQueryable<TEntity> whereQuery = null;

            foreach (var expression in includeExpression)
            {
                whereQuery = DbSet.Include(expression);
            }

            return whereQuery;
        }

        protected readonly DbSet<TEntity> DbSet;

        protected readonly TDbContext Context;

    }
}