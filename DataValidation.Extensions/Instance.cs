using System;
using System.Collections.Generic;

namespace DataValidation.Extensions
{
    public static class Instance<T>
    {
        public static ICollection<T> Add<TCollection>(ICollection<T> value, T item, Func<TCollection> newInstanceFunc = null)
            where TCollection : ICollection<T>
        {
            if(newInstanceFunc == null)
                throw new ArgumentNullException(nameof(newInstanceFunc));
            
            if(value == null)
                value = newInstanceFunc();

            value.Add(item);

            return value;
        }
        public static Func<ICollection<T>> AsGenericList() => () => new List<T>();
    }
}