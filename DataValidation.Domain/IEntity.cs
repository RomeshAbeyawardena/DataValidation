using System;

namespace DataValidation.Domains
{
    public interface IEntity<TKey> where TKey: IComparable<TKey>
    {
        TKey Id { get; set; }
    }
}
