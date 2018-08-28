using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using DataValidation.Domains;
using DataValidation.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace DataValidation.DataLayer
{
    public abstract class AsyncSqlEntityRepository<TContext, TEntity, TKey> : SqlEntityRepository<TContext, TEntity, TKey>, 
        IAsyncEntityRepository<TEntity, TKey> 
        where TContext : DbContext 
        where TEntity : class, IEntity<TKey> where TKey : IComparable<TKey>
    {
        protected AsyncSqlEntityRepository(TContext context) : base(context)
        {
            
        }


        public async Task<bool> AnyAsync(Expression<Func<TEntity, bool>> anyExpression)
        {
            return await DbSet.AnyAsync(anyExpression);
        }

        public async Task<int> CommitAsync(bool saveAllChanges = true, CancellationToken? cancellationToken = null)
        {
            if (cancellationToken == null)
                cancellationToken = CancellationToken.None;

            return await Context.SaveChangesAsync(saveAllChanges, cancellationToken.Value);
        }

        public async Task<IEnumerable<TEntity>> WhereAsync(Expression<Func<TEntity, bool>> whereExpression)
        {
            return await Where(whereExpression).ToListAsync();
        }

        public async Task<IEnumerable<TEntity>> WhereAsync<TSelector>(Expression<Func<TEntity, bool>> whereExpression,
            params Expression<Func<TEntity, TSelector>>[] includeExpression)
        {
            if (!includeExpression.Any())
                return await WhereAsync(whereExpression);

            return await IncludeMany(includeExpression).Where(whereExpression).ToListAsync();
        }

        public async Task<TEntity> IncludeFirstOrDefaultAsync(Expression<Func<TEntity, bool>> firstOrDefaultExpression,
            params Expression<Func<TEntity, object>>[] includeExpression)
        {
            if (!includeExpression.Any())
                return await DbSet.FirstOrDefaultAsync(firstOrDefaultExpression);

            return await IncludeMany(includeExpression).FirstOrDefaultAsync(firstOrDefaultExpression);
        }
        
        public async Task<TEntity> FirstOrDefaultAsync(Expression<Func<TEntity, bool>> firstOrDefaultExpression)
        {
            return await DbSet.FirstOrDefaultAsync(firstOrDefaultExpression);
        }

        public async Task<TEntity> FindAsync(TKey key)
        {
            return await DbSet.FindAsync(key);
        }
    }
}