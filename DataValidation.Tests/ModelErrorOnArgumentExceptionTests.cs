using System;
using System.Collections.Generic;
using DataValidation.Mvc;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using NUnit.Framework;

namespace DataValidation.Tests
{
    [TestFixture]
    public class ModelErrorOnArgumentExceptionTests
    {
        [Test]
        public void OnException_handles_ArgumentException()
        {
            SetupDependencies(new ArgumentException("ArgumentException","testClass"));
            _systemUnderTest.OnException(_exceptionContext);

            Assert.IsTrue(_modelStateDictionary.ErrorCount > 0);
            
            Assert.IsTrue(_exceptionContext.ExceptionHandled);
            Assert.IsTrue(_exceptionContext.Result is BadRequestObjectResult);
        }

        [Test]
        public void OnException_does_not_handle_Exception()
        {
            SetupDependencies(new InvalidOperationException());
            _systemUnderTest.OnException(_exceptionContext);

            Assert.IsTrue(_modelStateDictionary.ErrorCount == 0);
            
            Assert.IsFalse(_exceptionContext.ExceptionHandled);
            Assert.IsNull(_exceptionContext.Result);

            Assert_Fail();

            SetupDependencies(new ArgumentException("ArgumentException"));
            
            _systemUnderTest.OnException(_exceptionContext);
            
            Assert_Fail();
        }

        private void Assert_Fail()
        {
            Assert.IsTrue(_modelStateDictionary.ErrorCount == 0);
            
            Assert.IsFalse(_exceptionContext.ExceptionHandled);
            Assert.IsNull(_exceptionContext.Result);
        }

        private void SetupDependencies(Exception exception)
        {
            var filters = new List<IFilterMetadata>();
            _systemUnderTest = new ModelErrorOnArgumentException();
            _modelStateDictionary = new ModelStateDictionary();
            var actionContext = new TestActionContext(_modelStateDictionary);

            _exceptionContext =
                new ExceptionContext(actionContext, filters) {Exception = exception};
        }

        private ModelStateDictionary _modelStateDictionary;
        private ExceptionContext _exceptionContext;
        private ModelErrorOnArgumentException _systemUnderTest;
    }
}