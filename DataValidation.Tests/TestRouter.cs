using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Routing;

namespace DataValidation.Tests
{
    public class TestRouter : IRouter
    {
        public Task RouteAsync(RouteContext context)
        {
            throw new System.NotImplementedException();
        }

        public VirtualPathData GetVirtualPath(VirtualPathContext context)
        {
            throw new System.NotImplementedException();
        }
    }
}