using System;
using DataValidation.Domains;

namespace DataValidation.Interfaces
{
    public interface IMetaDataProvider
    {
        void ResolveMetaData<TEntityKey>(IEntity<TEntityKey> entity, bool isNew)
            where TEntityKey : IComparable<TEntityKey>;
    }
}