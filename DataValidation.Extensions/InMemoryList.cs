using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using DataValidation.Domains;
using DataValidation.Interfaces;

namespace DataValidation.Extensions
{
    public class InMemoryList<TCollection, TEntity, TKey> : IInMemoryList<TCollection, TEntity, TKey>
        where TCollection: ICollection<TEntity>
        where TEntity : IEntity<TKey> 
        where TKey : IComparable<TKey>
    {
        public InMemoryList(ICollection<TEntity> destinationCollection)
        {
            _destinationCollection = destinationCollection;
            _inMemoryDictionary = new Dictionary<string, InMemoryListItem<TEntity, TKey>>();
        }

        public InMemoryListItem<TEntity, TKey> this[string key] => _inMemoryDictionary[key];

        public TEntity Add(TEntity newItem)
        {
            SetItemState(newItem, ItemState.Added);
            return newItem;
        }

        public TEntity Update(TEntity updatedItem)
        {
            SetItemState(updatedItem, ItemState.Updated);
            return updatedItem;
        }

        public void Remove(TEntity removedItem)
        {
            SetItemState(removedItem, ItemState.Updated);
        }

        public int Commit()
        {
            var actionCount = 0;
            foreach (var inMemoryListItem in _inMemoryDictionary.Values)
            {
                CommitItem(inMemoryListItem);
                actionCount++;
            }

            _inMemoryDictionary.Clear();

            return actionCount;
        }


        public bool Any(Expression<Func<TEntity, bool>> whereExpression)
        {
            return CompileExpression(_destinationCollection, (a, b) => a.Any(b), whereExpression);
        }

        public TEntity Find(Expression<Func<TEntity, bool>> whereExpression)
        {
            return FirstOrDefault(whereExpression);
        }

        public IEnumerable<TEntity> Where(Expression<Func<TEntity, bool>> whereExpression)
        {
            return CompileExpression(_destinationCollection, (a, b) => a.Where(b), whereExpression);
        }

        public TEntity FirstOrDefault(Expression<Func<TEntity, bool>> whereExpression)
        {
            return CompileExpression(_destinationCollection, (a, b) => a.FirstOrDefault(b), whereExpression);
        }

        public IReadOnlyCollection<TEntity> ReadOnlyList => _inMemoryDictionary
            .Select(a => a.Value.Item).ToList().AsReadOnly();

        private TDestination CompileExpression<TSource, TDestination>(TSource source, 
            Func<TSource, Func<TEntity, bool>, TDestination> action, Expression<Func<TEntity, bool>> expression)
        where TSource : ICollection<TEntity>
        {
            return action(source, expression.Compile());
        }

        private void SetItemState(TEntity item, ItemState itemState)
        {
            var generatedKey = GenerateKey(item.Id);
            if (_inMemoryDictionary.ContainsKey(generatedKey))
                throw new ArgumentException($"Unable to queue duplicate key: { generatedKey }, one operation per unique entity", nameof(item));

            _inMemoryDictionary.Add(generatedKey, item.ToInMemoryListItem<TEntity, TKey>(itemState));
            
        }

        private string GenerateKey(TKey key)
        {
            return $"{ typeof(TEntity).Name }::{ key }";
        }

        private void CommitItem(InMemoryListItem<TEntity, TKey> item)
        {
            switch (item.ItemState)
            {
                case ItemState.Added:
                    _destinationCollection.Add(item.Item);
                    break;
                case ItemState.Updated:
                    UpdateItem(item.Item);
                    break;
                case ItemState.Deleted:
                    _destinationCollection.Remove(item.Item);
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(item.ItemState), item.ItemState, null);
            }
        }

        private void UpdateItem(TEntity updatedItem)
        {
            var repositoryList = new List<TEntity>(_destinationCollection);
            var itemIndex = repositoryList.IndexOf(updatedItem);
            
            if(itemIndex > 0)
                repositoryList[itemIndex] = updatedItem;

            throw new InvalidOperationException();
        }

        public IList<InMemoryListItem<TEntity, TKey>> InMemoryListItems => _inMemoryDictionary.Select(a => a.Value).ToList();


        private readonly ICollection<TEntity> _destinationCollection;
        private readonly IDictionary<string, InMemoryListItem<TEntity, TKey>> _inMemoryDictionary;

    }
}