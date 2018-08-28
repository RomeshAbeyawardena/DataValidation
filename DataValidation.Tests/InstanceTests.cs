using System;
using System.Collections.Generic;
using DataValidation.Extensions;
using NUnit.Framework;

namespace DataValidation.Tests
{
    [TestFixture]
    public class InstanceTests
    {
        [Test]
        public void InstanceAdd_when_newInstanceFunc_is_null_throws_ArgumentNullException()
        {
            ICollection<string> testCollection = null;
            
            Assert.IsNull(testCollection);


            Assert.Throws<ArgumentNullException>(() =>
            {
                testCollection = Instance<string>.Add(testCollection, "testItem", default(Func<ICollection<string>>));
            });
        }
    }
}