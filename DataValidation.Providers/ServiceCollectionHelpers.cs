using System;
using DataValidation.Domains;
using DataValidation.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace DataValidation.Providers
{
    public static class ServiceCollectionHelpers
    {
        public static IServiceCollection RegisterBrokerServices(this IServiceCollection serviceCollection)
        {
            if(serviceCollection == null)
                throw new ArgumentNullException(nameof(serviceCollection));

            var serviceProvider = serviceCollection.BuildServiceProvider();

            var connectionInfo = serviceProvider.GetService(typeof(IConnectionInfo));

            if(connectionInfo == null)
                throw new ArgumentException("Implementation of IConnectionInfo not found!");

            var service = serviceProvider.GetService(typeof(IServiceProviderFactory));
            
            if(service == null)
                throw new ArgumentException("Implementation of IServiceProviderFactory not found!");

            return ((IServiceProviderFactory) service).RegisterAll(serviceCollection, (IConnectionInfo)connectionInfo);
        }
    }
}