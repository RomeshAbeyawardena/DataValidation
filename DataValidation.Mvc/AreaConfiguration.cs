using DataValidation.Interfaces;
using Microsoft.AspNetCore.Routing;
using Microsoft.AspNetCore.Builder;

namespace DataValidation.Mvc
{
    public class AreaConfiguration : IAreaConfiguration
    {
        public static IAreaConfiguration DefaultAreaConfiguration = new AreaConfiguration
        {
            DefaultAreaRouteName = "defaultAreaRoute",
            DefaultAreaRouteTemplate = "{area:exists}/{controller}/{action}/{id?}"
        };
        public string DefaultAreaRouteName { get; set; }
        public string DefaultAreaRouteTemplate { get; set; }
        public virtual IRouteBuilder RegisterAreas(IRouteBuilder routeBuilder)//, string defaultAreaRoute = "{area:exists}/{controller}/{action}/{id?}")
        {
            //"defaultAreaRoute"
            return routeBuilder.MapRoute(DefaultAreaRouteName, DefaultAreaRouteTemplate);
        }
    }
}