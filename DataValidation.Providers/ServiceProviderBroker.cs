using System;
using System.Collections.Generic;
using DataValidation.Domains;
using DataValidation.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Internal;
using IServiceProvider = DataValidation.Interfaces.IServiceProvider;

namespace DataValidation.Providers
{
    public abstract class ServiceProviderBroker : IServiceProviderBroker
    {
        public virtual IEnumerable<IServiceProvider> GetServiceProviders()
        {
            var serviceProviders = new List<IServiceProvider> { ServiceProvider.Assign<BaseServices>() };

            return serviceProviders.ToArray();
        }
    }

    public class BaseServices : IServiceProvider
    {
        public IServiceCollection RegisterServices(IServiceCollection services, IConnectionInfo connectionInfo)
        {
            return services.AddSingleton<ISystemClock, SystemClock>()
                .AddSingleton<IClockProvider, ClockProvider>()
                .AddSingleton<IMetaDataProvider, MetaDataProvider>();

        }
    }
}