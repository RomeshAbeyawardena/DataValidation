using System.Collections.Generic;
using DataValidation.Domains;
using DataValidation.Interfaces;
using DataValidation.Providers;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using NUnit.Framework;
using ServiceProvider = DataValidation.Providers.ServiceProvider;

namespace DataValidation.Tests
{
    [TestFixture]
    public class ServiceProviderFactoryTests
    {
        [Test]
        public void RegisterAll_Calls()
        {
            SetupDependencies(new []
            {
                ServiceProvider.Assign<TestServiceProvider>() 
            });

            _serviceCollectionMock.Setup(serviceCollection => serviceCollection.Add(It.IsAny<ServiceDescriptor>())).Verifiable();

            _systemUnderTest.RegisterAll(_serviceCollectionMock.Object, _connectionInfoMock.Object);

            _serviceCollectionMock.Verify(serviceCollection => serviceCollection.Add(It.IsAny<ServiceDescriptor>()));
        }

        private void SetupDependencies(IEnumerable<IServiceProvider> serviceProviders)
        {
            _serviceProviderBrokerMock = new Mock<IServiceProviderBroker>();
            _serviceCollectionMock = new Mock<IServiceCollection>();
            _connectionInfoMock = new Mock<IConnectionInfo>();

            _serviceProviderBrokerMock.Setup(serviceProviderBroker => serviceProviderBroker.GetServiceProviders())
                .Returns(serviceProviders);

            _systemUnderTest = new ServiceProviderFactory(_serviceProviderBrokerMock.Object);

        }

        private Mock<IConnectionInfo> _connectionInfoMock;
        private Mock<IServiceProviderBroker> _serviceProviderBrokerMock;
        private Mock<IServiceCollection> _serviceCollectionMock;
        private IServiceProviderFactory _systemUnderTest;

        private interface IMyServiceThatConsumesIConnectionInfo
        {
            string ConnectionInfo { get; }
        }

        private class MyServiceThatConsumesIConnectionInfo : IMyServiceThatConsumesIConnectionInfo
        {
            private readonly IConnectionInfo _connectionInfo;

            public MyServiceThatConsumesIConnectionInfo(IConnectionInfo connectionInfo)
            {
                _connectionInfo = connectionInfo;
            }

            public string ConnectionInfo => _connectionInfo.ConnectionString;
        }

        private class TestServiceProvider : IServiceProvider
        {
            public IServiceCollection RegisterServices(IServiceCollection services, IConnectionInfo connectionInfo)
            {
                return services.AddSingleton<IMyServiceThatConsumesIConnectionInfo, 
                    MyServiceThatConsumesIConnectionInfo>(a => new MyServiceThatConsumesIConnectionInfo(connectionInfo));
            }
        }
    }
}