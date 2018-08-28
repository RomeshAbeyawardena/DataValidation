using System;
using DataValidation.Domains;

namespace DataValidation.Extensions
{
    public static class EntityHelpers
    {
        public static InMemoryListItem<TEntity,TKey> ToInMemoryListItem<TEntity, TKey>(this TEntity value, ItemState itemState) 
            where TEntity : IEntity<TKey> 
            where TKey : IComparable<TKey>
        {
            return new InMemoryListItem<TEntity, TKey>(value, itemState);
        }
    }
}