using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using DataValidation.Domains;

namespace DataValidation.Interfaces
{
    public interface IAsyncEntityRepository<TEntity, in TKey> : IEntityRepository<TEntity, TKey>
        where TEntity: class, IEntity<TKey> where TKey: IComparable<TKey>
    {
        Task<bool> AnyAsync(Expression<Func<TEntity, bool>> anyExpression);
        Task<int> CommitAsync(bool saveAllChanges = true, CancellationToken? cancellationToken = null);
        Task<TEntity> IncludeFirstOrDefaultAsync(Expression<Func<TEntity, bool>> firstOrDefaultExpression, params Expression<Func<TEntity, object>>[] includeExpression);
        Task<TEntity> FirstOrDefaultAsync(Expression<Func<TEntity, bool>> firstOrDefaultExpression);
        Task<TEntity> FindAsync(TKey key);
        Task<IEnumerable<TEntity>> WhereAsync(Expression<Func<TEntity, bool>> whereExpression);
        Task<IEnumerable<TEntity>> WhereAsync<TSelector>(Expression<Func<TEntity, bool>> whereExpression, params Expression<Func<TEntity, TSelector>>[] includeExpression);
    }
}