using System;
using System.Collections.Generic;

namespace DataValidation.Extensions
{
    public static class CollectionHelpers
    {
        /// <summary>
        /// Creates a new collection based on the newInstanceFunc parameter if the specified ICollection instance is null or uses current ICollection to add item.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="TCollection"></typeparam>
        /// <param name="value"></param>
        /// <param name="item"></param>
        /// <param name="newInstanceFunc"></param>
        /// <returns>Returns a new collection, if the specified collection is null otherwise returns the specified collection.</returns>
        public static ICollection<T> Add<T, TCollection>(this ICollection<T> value, T item,  Func<TCollection> newInstanceFunc)
            where TCollection: ICollection<T>
        {
            if(newInstanceFunc == null)
                throw new ArgumentNullException(nameof(newInstanceFunc));

            return Instance<T>.Add(value, item, newInstanceFunc);
        }
    }
}
