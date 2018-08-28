using System.Collections.Generic;
using DataValidation.Domains;

namespace DataValidation.Interfaces
{
    public interface IServiceProviderBroker
    {
        IEnumerable<IServiceProvider> GetServiceProviders();
    }
}