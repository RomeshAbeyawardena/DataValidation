using Microsoft.Extensions.DependencyInjection;
using Moq;
using NUnit.Framework;
using NUnit.Framework.Internal;

namespace DataValidation.Tests
{
    [TestFixture]
    public class ServiceCollectionHelpersTests
    {
        [Test]
        public void RegisterBrokerServices_returns_instance()
        {
            _serviceCollectionMock = new Mock<IServiceCollection>();
        }

        private Mock<IServiceCollection> _serviceCollectionMock;
    }
}