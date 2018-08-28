using System;
using DataValidation.Domains;

namespace DataValidation.Tests
{
    public class MyTestEntity : IEntity<int>, ICreated, IModified, IVisible
    {
        public int Id { get; set; }

        public override bool Equals(object obj)
        {
            if (! (obj is MyTestEntity myTestEntity) )
                return false;
            return myTestEntity.Id.Equals(Id);
        }

        public DateTime Created { get; set; }
        public DateTime Modified { get; set; }
        public bool IsActive { get; set; }
    }
}