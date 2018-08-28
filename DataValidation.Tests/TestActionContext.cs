using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Abstractions;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Routing;

namespace DataValidation.Tests
{
    public class TestActionContext : ActionContext
    {
        public TestActionContext(ModelStateDictionary modelStateDictionary)
            : base(new TestHttpContext(), new RouteData(), new ActionDescriptor(), modelStateDictionary)
        {

        }
    }
}