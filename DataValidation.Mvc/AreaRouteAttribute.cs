using DataValidation.Domains;
using DataValidation.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace DataValidation.Mvc
{
    public class AreaRouteAttribute : RouteAttribute
    {
        public const string DefaultAreaRouteTemplate = "[controller]/[action]";
        public AreaRouteAttribute(string areaName, string defaultAreaRoute = DefaultAreaRouteTemplate) 
            : base($"{areaName}/{defaultAreaRoute}")
        {
            Area = areaName;
        }
        public string Area { get; set; }
    }
}