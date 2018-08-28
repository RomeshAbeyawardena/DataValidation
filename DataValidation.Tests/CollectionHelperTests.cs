using System;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using DataValidation.Extensions;

namespace DataValidation.Tests
{
    [TestFixture]
    public class CollectionHelperTests
    {
        [Test]
        public void InstanceAdd_when_newInstanceFunc_is_null_throws_ArgumentNullException()
        {
            ICollection<string> testCollection = null;
            
            Assert.IsNull(testCollection);


            Assert.Throws<ArgumentNullException>(() =>
            {
                testCollection = testCollection.Add("testItem", default(Func<ICollection<string>>));
            });
        }

        [Test]
        public void InstanceAdd_when_collection_is_null_creates_instance_then_adds_item()
        {
            ICollection<string> testCollection = null;
            
            Assert.IsNull(testCollection);
            
            testCollection = testCollection.Add("testItem", Instance<string>.AsGenericList());

            Assert.IsNotNull(testCollection);
            Assert.Contains("testItem", testCollection.ToList());
        }

        [Test]
        public void InstanceAdd_when_collection_is__not_null_adds_item()
        {
            ICollection<string> testCollection = new List<string>
            {
                "Cow",
                "Pig"
            };
            
            testCollection = testCollection.Add("testItem", Instance<string>.AsGenericList());

            Assert.Contains("Cow", testCollection.ToList());
            Assert.Contains("Pig", testCollection.ToList());
            Assert.Contains("testItem", testCollection.ToList());
        }
    }
}
