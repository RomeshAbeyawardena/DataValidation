using System;
using System.Threading.Tasks;
using DataValidation.Extensions;
using NUnit.Framework;
using NUnit.Framework.Internal;

namespace DataValidation.Tests
{
    [TestFixture]
    public class ConditionalExceptionTests
    {
        [Test]
        public void Run_throws_Exception()
        {
            var expectedDate = new DateTime(2018, 01, 01);

            var myEntity = new MyTestEntity();

            var systemUnderTest =
                ConditionalException<MyTestEntity>.Create( 
                    async(myTestEntity) => await Task.FromResult(myTestEntity.Created.Equals(expectedDate)), 
                    typeof(InvalidTimeZoneException));

            Assert.ThrowsAsync<InvalidTimeZoneException>( async() => await systemUnderTest.Run(myEntity) );

        }

        [Test]
        public void Run_returns_myEntity()
        {
            var expectedDate = new DateTime(2018, 01, 01);

            var myEntity = new MyTestEntity
            { 
               Created = expectedDate
            };

            var systemUnderTest =
                ConditionalException<MyTestEntity>.Create(
                    async (myTestEntity) => await Task.FromResult(myTestEntity.Created.Equals(expectedDate)),
                    typeof(InvalidTimeZoneException));
            
            Assert.DoesNotThrowAsync(async () => await systemUnderTest.Run(myEntity));
        }
    }
}