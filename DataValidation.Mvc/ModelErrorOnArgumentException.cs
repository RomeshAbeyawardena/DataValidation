using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace DataValidation.Mvc
{
    public class ModelErrorOnArgumentException : ExceptionFilterAttribute
    {
        public override void OnException(ExceptionContext exceptionContext)
        {
            if(exceptionContext.Exception is ArgumentException argumentException)
                HandleException(exceptionContext, argumentException);
        }

        protected virtual void HandleException(ExceptionContext exceptionContext, ArgumentException exception)
        {
            if (string.IsNullOrEmpty(exception.ParamName))
                return;

            exceptionContext.ModelState.AddModelError(exception.ParamName, exception.Message);
            exceptionContext.ExceptionHandled = true;
            exceptionContext.Result = new BadRequestObjectResult(exceptionContext.ModelState);
        }
    }
}