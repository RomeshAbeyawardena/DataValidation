using System;

namespace DataValidation.Extensions
{
    public static class ObjectHelper
    {
        public static TValue SetIf<TValue>(this TValue value, Func<TValue, bool> ifFunc, TValue setValue)
        {
            return ifFunc(value) ? setValue : value;
        }
    }
}