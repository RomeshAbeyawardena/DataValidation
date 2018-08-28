using System;
using DataValidation.Domains;

namespace DataValidation.Providers
{
    public static class ServiceProvider
    {
        public static T Assign<T>() where T: Interfaces.IServiceProvider
        {
            return Activator.CreateInstance<T>();
        }
    }
}