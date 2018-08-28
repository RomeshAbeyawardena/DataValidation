using System.Collections.Generic;
using System.Linq;
using DataValidation.Domains;
using DataValidation.Interfaces;
using DataValidation.Providers;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using NUnit.Framework;

namespace DataValidation.Tests
{
    [TestFixture]
    public class ServiceProviderBrokerTests
    {
        [Test]
        public void GetServiceProviders_returns_BaseServices()
        {
            SetupDependencies();

            var serviceProviders =_systemUnderTest.GetServiceProviders().ToArray();
            Assert.IsNotEmpty(serviceProviders);
            Assert.IsAssignableFrom<BaseServices>(serviceProviders[0]);
        }

        [Test]
        public void GetServiceProviders_returns_IServiceProviders_from_override()
        {
            SetupDependencies();

            var serviceProviders =_systemUnderTest.GetServiceProviders().ToArray();
            Assert.IsNotEmpty(serviceProviders);
            Assert.IsAssignableFrom<TestServiceProvider>(serviceProviders[1]);
        }

        private void SetupDependencies()
        {
            _systemUnderTest = new TestServiceProviderBroker();
        }

        private IServiceProviderBroker _systemUnderTest;
        
        private class TestServiceProviderBroker : ServiceProviderBroker
        {
            public override IEnumerable<IServiceProvider> GetServiceProviders()
            {
                return new List<IServiceProvider>(base.GetServiceProviders())
                {
                    Providers.ServiceProvider.Assign<TestServiceProvider>()
                };
            }
        }

        private class TestServiceProvider : IServiceProvider
        {
            public IServiceCollection RegisterServices(IServiceCollection services, IConnectionInfo connectionInfo)
            {
                return services.AddSingleton<IList<string>, List<string>>();
            }
        }
    }
}