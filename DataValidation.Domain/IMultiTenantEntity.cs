using System;

namespace DataValidation.Domains
{
    public interface IMultiTenantEntity<TAccountEntity, TAccountKey, TKey> : IEntity<TKey> 
        where TKey: IComparable<TKey>
        where TAccountKey: IComparable<TAccountKey>
        where TAccountEntity: IEntity<TAccountKey>

    {
        TAccountEntity Account { get; set; }
        TAccountKey AccountId { get; set; }
    }
}