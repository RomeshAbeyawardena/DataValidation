using DataValidation.Domains;
using Microsoft.Extensions.DependencyInjection;

namespace DataValidation.Interfaces
{
    public interface IServiceProvider
    {
        IServiceCollection RegisterServices(IServiceCollection services, IConnectionInfo connectionInfo);
    }
}