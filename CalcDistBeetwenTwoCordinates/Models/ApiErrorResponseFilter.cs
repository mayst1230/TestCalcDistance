using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Linq;

namespace CalcDistBeetwenTwoCordinates.Models
{
    /// <summary>
    /// Фильтр для операций API для обработки исключений.
    /// </summary>
    public class ApiErrorResponseFilter : IActionFilter, IOrderedFilter
    {
        public int Order => int.MaxValue - 10;

        public void OnActionExecuting(ActionExecutingContext context) { }

        public void OnActionExecuted(ActionExecutedContext context)
        {
            if (context.Exception is Exceptions.ApiException exception)
            {
                if (!string.IsNullOrEmpty(exception.Message))
                {
                    context.ModelState.AddModelError(exception.ModelField ?? string.Empty, exception.Message);
                }
                var errDict = context.ModelState.Where(i => i.Value.Errors.Count > 0).Select(i => new
                {
                    Field = i.Key,
                    Errors = i.Value.Errors.Select(_ => _.ErrorMessage).ToArray()
                }).ToDictionary(i => i.Field, i => i.Errors);

                context.Result = new BadRequestObjectResult(new ApiErrorResponse()
                {
                    Errors = errDict
                });

                context.ExceptionHandled = true;
            }
        }
    }
}
