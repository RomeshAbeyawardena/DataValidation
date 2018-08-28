using System;
using DataValidation.Interfaces;
using DataValidation.Providers;
using Moq;
using NUnit.Framework;

namespace DataValidation.Tests
{
    [TestFixture]
    public class MetaDataProviderTests
    {
        [Test]
        public void MetaDataProvider_throws()
        {
            SetupDependencies();

            var myTestEntity = new MyTestEntity();

            _metaDataProvider = new MetaDataProvider(_clockProviderMock.Object);

            Assert.Throws<ArgumentNullException>(() => { _metaDataProvider.ResolveMetaData(default(MyTestEntity), false); });
            Assert.Throws<ArgumentNullException>(() => { _metaDataProvider.ResolveMetaData(myTestEntity, false); });

            _clockProviderMock.Setup(clockProvider => clockProvider.DateTime).Returns(new DateTime(2017, 01, 01, 12, 30, 0));

            Assert.Throws<InvalidOperationException>(() => { _metaDataProvider.ResolveMetaData(myTestEntity, false); });
        }
        [Test]
        public void MetaDataProvider_resolves_valid_meta_data()
        {
            SetupDependencies();

            var myTestEntity = new MyTestEntity();
            var expectedDate = new DateTime(2017, 01, 01, 12, 30, 00);

            _metaDataProvider = new MetaDataProvider(_clockProviderMock.Object);
            _clockProviderMock.Setup(clockProvider => clockProvider.DateTime).Returns(expectedDate);

            _metaDataProvider.ResolveMetaData(myTestEntity, true);

            Assert.AreEqual(expectedDate, myTestEntity.Created);
            Assert.AreEqual(expectedDate, myTestEntity.Modified);
            Assert.AreEqual(true, myTestEntity.IsActive);

            var expectedNewDate = new DateTime(2017, 01, 01, 13, 30, 00);
            _clockProviderMock.Setup(clockProvider => clockProvider.DateTime).Returns(expectedNewDate);

            myTestEntity.IsActive = false;

            _metaDataProvider.ResolveMetaData(myTestEntity, false);

            Assert.AreEqual(expectedDate, myTestEntity.Created);
            Assert.AreEqual(expectedNewDate, myTestEntity.Modified);
            Assert.AreEqual(false, myTestEntity.IsActive);
        }

        private void SetupDependencies()
        {
            _clockProviderMock = new Mock<IClockProvider>();
        }

        private Mock<IClockProvider> _clockProviderMock;
        private IMetaDataProvider _metaDataProvider;
    }
}