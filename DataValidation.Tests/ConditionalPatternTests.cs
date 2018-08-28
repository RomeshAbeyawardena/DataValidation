using System;
using System.Threading.Tasks;
using DataValidation.Extensions;
using DataValidation.Interfaces;
using Moq;
using NUnit.Framework;
using NUnit.Framework.Internal;

namespace DataValidation.Tests
{
    [TestFixture]
    public class ConditionalPatternTests
    {
        [Test]
        public async Task RunConditionalPattern_with_one_patternConditionalDelegates_calls_run()
        {
            
            _conditionalExceptionMock = new Mock<IConditionalException<MyTestEntity>>();
            _conditionalExceptionMock.Setup(a => a.Run(It.IsAny<MyTestEntity>())).Returns(Task.FromResult(true)).Verifiable();

            var myTestEntity = new MyTestEntity();
            await ConditionalPattern<MyTestEntity>.RunConditionalPattern(myTestEntity, async (testEntity) =>
                {
                    await Task.Delay(1); 
                    return testEntity;
                },
                async (testEntity) =>
                {
                    await Task.Delay(1); 
                },
                _conditionalExceptionMock.Object);

            _conditionalExceptionMock.Verify(a => a.Run(It.IsAny<MyTestEntity>()), Times.Exactly(1));

        }

        [Test]
        public void RunConditionalPattern_when_patternConditionalDelegates_throws_exception()
        {

            _conditionalExceptionMock = new Mock<IConditionalException<MyTestEntity>>();
            _conditionalExceptionMock.Setup(a => a.Run(It.IsAny<MyTestEntity>())).Throws<InvalidOperationException>();
            var myTestEntity = new MyTestEntity();
            Assert.ThrowsAsync<InvalidOperationException>(async() =>
            {
                await ConditionalPattern<MyTestEntity>.RunConditionalPattern(myTestEntity,
                     async (testEntity) =>
                    {
                        await Task.Delay(1);
                        Assert.Fail();
                        return testEntity;
                        
                    },
                    async (testEntity) =>
                    {
                        await Task.Delay(1);
                        Assert.Pass();
                    }, 
                    _conditionalExceptionMock.Object);
            });
        }

        [Test]
        public async Task RunConditionalPattern_when_patternConditionalDelegates_invokes_passDelegate()
        {

            _conditionalExceptionMock = new Mock<IConditionalException<MyTestEntity>>();
            _conditionalExceptionMock.Setup(a => a.Run(It.IsAny<MyTestEntity>())).Returns(Task.FromResult(true));
            var myTestEntity = new MyTestEntity();
            
                await ConditionalPattern<MyTestEntity>.RunConditionalPattern(myTestEntity,
                    async (testEntity) =>
                    {
                        await Task.Delay(1);
                        Assert.Pass();
                        return testEntity;

                    },
                    async (testEntity) =>
                    {
                        await Task.Delay(1);
                        Assert.Fail();
                    },
                    _conditionalExceptionMock.Object);
            
        }

        [Test]
        public void RunConditionalPattern_when_entity_is_null_throws_exception()
        {
            var expectedDate = new DateTime(2018, 01, 01);
            var myTestEntity = new MyTestEntity();

            Assert.ThrowsAsync<ArgumentNullException>(async() => {
                await ConditionalPattern<MyTestEntity>.RunConditionalPattern(null,
                    async (testEntity) => await Task.FromResult(testEntity.Created.Equals(expectedDate)),
                    async (testEntity) =>
                    {
                        await Task.Delay(1);
                        return myTestEntity;
                    },
                    async (testEntity) =>
                    {
                        await Task.Delay(1);
                    });
            });
            
        }

        [Test]
        public async Task RunConditionalPattern_when_entity_is_not_null_calls_pass()
        {
            var expectedDate = new DateTime(2018, 01, 01);
            var myTestEntity = new MyTestEntity
            {
                Created = expectedDate
            };
            var passCalled = false;

            await ConditionalPattern<MyTestEntity>.RunConditionalPattern(myTestEntity,
                async (testEntity) => await Task.FromResult(testEntity.Created.Equals(expectedDate)),
                async (testEntity) =>
                {
                    await Task.Delay(1);
                    passCalled = true;
                    return myTestEntity;
                },
                async (testEntity) =>
                {
                    await Task.Delay(1);
                });

            Assert.True(passCalled);
        }


        [Test]
        public async Task RunConditionalPattern_when_entity_is_not_null_calls_fail()
        {
            var expectedDate = new DateTime(2018, 01, 01);
            var myTestEntity = new MyTestEntity();

            var failCalled = false;

            await ConditionalPattern<MyTestEntity>.RunConditionalPattern(myTestEntity,
                async (testEntity) => await Task.FromResult(testEntity.Created.Equals(expectedDate)),
                async (testEntity) =>
                {
                    await Task.Delay(1);
                    return myTestEntity;
                },
                async (testEntity) =>
                {
                    failCalled = true;
                    await Task.Delay(1);
                });

            Assert.True(failCalled);
        }

        private void SetupDependencies()
        {

        }

        private Mock<IConditionalException<MyTestEntity>> _conditionalExceptionMock;

        
    }
}