using System.Collections.Generic;
using System.Linq;
using DataValidation.Domains;
using DataValidation.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace DataValidation.Providers
{
    public class ServiceProviderFactory : IServiceProviderFactory
    {
        public ServiceProviderFactory(IServiceProviderBroker serviceProviderBroker)
        {
            _serviceProviders = serviceProviderBroker.GetServiceProviders();
        }

        public virtual IServiceCollection RegisterAll(IServiceCollection services, IConnectionInfo connectionInfo)
        {
            foreach (var serviceProvider in _serviceProviders)
            {
                services = serviceProvider.RegisterServices(services, connectionInfo);
            }

            return services;
        }

        private readonly IEnumerable<IServiceProvider> _serviceProviders;
    }
}