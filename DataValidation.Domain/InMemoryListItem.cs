using System;

namespace DataValidation.Domains
{
    public enum ItemState { Added, Updated, Deleted }
    public class InMemoryListItem<TEntity, TKey> where TEntity : IEntity<TKey> where TKey : IComparable<TKey>
    {
        public TKey Id { get; }
        public TEntity Item { get; }
        public ItemState ItemState { get; }

        public InMemoryListItem(TEntity item, ItemState itemState)
        {
            if (item == null)
                throw new ArgumentNullException(nameof(item));

            Id = item.Id;
            Item = item;
            ItemState = itemState;
        }
    }
}