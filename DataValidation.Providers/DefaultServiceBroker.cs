using System.Collections.Generic;
using DataValidation.Interfaces;

namespace DataValidation.Providers
{
    public class DefaultServiceBroker : ServiceProviderBroker
    {
        public DefaultServiceBroker(params IServiceProvider[] serviceProviders)
        {
            _serviceProviders = serviceProviders;
        }

        public override IEnumerable<IServiceProvider> GetServiceProviders()
        {
            var amendedServiceProvidersList = new List<IServiceProvider>(base.GetServiceProviders());
                amendedServiceProvidersList.AddRange(_serviceProviders);

            return amendedServiceProvidersList.ToArray();
        }

        private readonly IEnumerable<IServiceProvider> _serviceProviders;
    }
}