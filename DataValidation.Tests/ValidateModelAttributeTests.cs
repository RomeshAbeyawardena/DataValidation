using System.Collections.Generic;
using DataValidation.Mvc;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using NUnit.Framework;

namespace DataValidation.Tests
{
    [TestFixture]
    public class ValidateModelAttributeTests
    {
        [Test]
        public void OnActionExecuting_Result_returns_BadRequestObjectResult()
        {
            _systemUnderTest = new ValidateModelAttribute();
            var modelStateDictionary = new ModelStateDictionary();
            var actionContext = new TestActionContext(modelStateDictionary);

            var actionExecutingContext = new ActionExecutingContext(actionContext, new List<IFilterMetadata>(),
                new Dictionary<string, object>(), null);

            modelStateDictionary.AddModelError("name", "IsInvalid");

            _systemUnderTest.OnActionExecuting(actionExecutingContext);
            Assert.IsAssignableFrom<BadRequestObjectResult>(actionExecutingContext.Result); 
        }

        [Test]
        public void OnActionExecuting_Result_is_null()
        {
            _systemUnderTest = new ValidateModelAttribute();
            var modelStateDictionary = new ModelStateDictionary();
            var actionContext = new TestActionContext(modelStateDictionary);

            var actionExecutingContext = new ActionExecutingContext(actionContext, new List<IFilterMetadata>(),
                new Dictionary<string, object>(), null);
            
            _systemUnderTest.OnActionExecuting(actionExecutingContext);
            Assert.IsNull(actionExecutingContext.Result); 
            _systemUnderTest.OnActionExecuted(new ActionExecutedContext(actionContext, new List<IFilterMetadata>(), null));
            Assert.IsNull(actionExecutingContext.Result); 
        }

        private ValidateModelAttribute _systemUnderTest;
    }
}