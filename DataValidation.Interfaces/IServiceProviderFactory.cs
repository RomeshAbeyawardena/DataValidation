using DataValidation.Domains;
using Microsoft.Extensions.DependencyInjection;

namespace DataValidation.Interfaces
{
    public interface IServiceProviderFactory
    {
        IServiceCollection RegisterAll(IServiceCollection services, IConnectionInfo connectionInfo);
    }
}