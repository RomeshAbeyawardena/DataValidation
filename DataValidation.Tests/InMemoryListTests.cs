using System;
using System.Collections.Generic;
using DataValidation.Domains;
using DataValidation.Extensions;
using DataValidation.Interfaces;
using NUnit.Framework;

namespace DataValidation.Tests
{
    public class InMemoryListTests
    {
        [Test]
        public void Add_success()
        {
            SetupDependencies(new List<MyTestEntity>());

            var myAddedTestEntity = new MyTestEntity {Id = 1};

            _systemUnderTest.Add(myAddedTestEntity);

            var addedInMemoryListItem = _systemUnderTest["MyTestEntity::1"];

            Assert.IsNotNull(
                addedInMemoryListItem);

            Assert.AreEqual(ItemState.Added, addedInMemoryListItem.ItemState);
        }

        [Test]
        public void Add_fails()
        {
            SetupDependencies(new List<MyTestEntity>());

            var myAddedTestEntity = new MyTestEntity {Id = 1};

            Assert.Throws<ArgumentException>(() =>
            {
                _systemUnderTest.Add(myAddedTestEntity);
                _systemUnderTest.Add(myAddedTestEntity);
            });
        }
        [Test]
        public void Update_success()
        {
            SetupDependencies(new List<MyTestEntity>());

            var myAddedTestEntity = new MyTestEntity {Id = 1};

            _systemUnderTest.Update(myAddedTestEntity);

            var addedInMemoryListItem = _systemUnderTest["MyTestEntity::1"];

            Assert.IsNotNull(
                addedInMemoryListItem);

            Assert.AreEqual(ItemState.Updated, addedInMemoryListItem.ItemState);
        }

        [Test]
        public void Update_fails()
        {
            SetupDependencies(new List<MyTestEntity>());

            var myAddedTestEntity = new MyTestEntity {Id = 1};

            Assert.Throws<ArgumentException>(() =>
            {
                _systemUnderTest.Update(myAddedTestEntity);
                _systemUnderTest.Update(myAddedTestEntity);
            });
        }

        public void Remove()
        {

        }
        public void Commit()
        {

        }
        public void Any()
        {

        }
        public void Find()
        {

        }
        public void Where()
        {

        }

        public void FirstOrDefault()
        {

        }



        private void SetupDependencies(ICollection<MyTestEntity> testData)
        {
            _systemUnderTest = new InMemoryList<IList<MyTestEntity>, MyTestEntity, int>(testData);
        }

        private IInMemoryList<IList<MyTestEntity>,MyTestEntity,int> _systemUnderTest;
    }

    
}