using Microsoft.AspNetCore.Routing;

namespace DataValidation.Interfaces
{
    public interface IAreaConfiguration
    {
        string DefaultAreaRouteName { get; set; }
        string DefaultAreaRouteTemplate { get; set; }

        IRouteBuilder RegisterAreas(IRouteBuilder routeBuilder);
    }
}