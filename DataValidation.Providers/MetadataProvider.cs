using System;
using System.Data.SqlTypes;
using System.Globalization;
using DataValidation.Domains;
using DataValidation.Extensions;
using DataValidation.Interfaces;

namespace DataValidation.Providers
{
    public class MetaDataProvider : IMetaDataProvider
    {
        private readonly IClockProvider _clockProvider;

        public MetaDataProvider(IClockProvider clockProvider)
        {
            _clockProvider = clockProvider;
        }
        public void ResolveMetaData<TEntityKey>(IEntity<TEntityKey> entity, bool isNew) where TEntityKey : IComparable<TEntityKey>
        {
            var dateTime = _clockProvider.DateTime;

            var minimumDate = SqlDateTime.MinValue.Value;

            if (entity == null)
                throw new ArgumentNullException(nameof(entity));

            if (dateTime < minimumDate)
                throw new ArgumentNullException(nameof(entity));

            if (entity is ICreated newEntityCreated)
            {
                if(isNew)
                    newEntityCreated.Created = 
                        newEntityCreated.Created.SetIf(e => e < minimumDate, dateTime);

                if(newEntityCreated.Created < minimumDate) 
                    throw new InvalidOperationException($"Can not resolve a datetime value below { minimumDate.ToString(CultureInfo.InvariantCulture) }");
            }
                
            if (entity is IModified newEntityModified)
                newEntityModified.Modified = 
                    newEntityModified.Modified = dateTime;

            if (isNew && entity is IVisible visibleEntity)
                visibleEntity.IsActive = true;
        }
    }
}