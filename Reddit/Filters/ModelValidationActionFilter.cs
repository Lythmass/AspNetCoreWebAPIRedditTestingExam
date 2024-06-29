using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Reddit.Filters
{
    public class ModelValidationActionFilter : IActionFilter
    {
        public void OnActionExecuted(ActionExecutedContext context)
        {
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
            var ModelState = context.ModelState;
            if (!ModelState.IsValid)
            {
                context.Result = new BadRequestObjectResult(ModelState);
            }
        }
    }
}
