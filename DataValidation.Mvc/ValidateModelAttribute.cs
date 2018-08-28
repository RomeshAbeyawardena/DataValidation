using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace DataValidation.Mvc
{
    public class ValidateModelAttribute : Attribute, IActionFilter
    {
        public void OnActionExecuting(ActionExecutingContext context)
        {
            if (!context.ModelState.IsValid)
                context.Result = ValidationProblem(context.ModelState);
        }
        
        public void OnActionExecuted(ActionExecutedContext context)
        {
        }

        private static IActionResult ValidationProblem(ModelStateDictionary modelStateDictionary)
        {
            return new BadRequestObjectResult(new ValidationProblemDetails(modelStateDictionary));
        }
    }
}
