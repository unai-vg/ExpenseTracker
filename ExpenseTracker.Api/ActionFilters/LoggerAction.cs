using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Logging;

namespace ExpenseTracker.Api.ActionFilters
{
    public class LoggerAction : ActionFilterAttribute
    {
        readonly ILogger<LoggerAction> _logger;
        public LoggerAction(ILogger<LoggerAction> logger)
        {
            _logger = logger;
        }
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            Log("OnActionExecuting", filterContext.RouteData);
        }

        public override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            Log("OnActionExecuted", filterContext.RouteData);
        }

        private void Log(string methodName, RouteData routeData)
        {
            var controller = routeData.Values["controller"];
            var action = routeData.Values["action"];
            _logger.LogInformation($"{methodName} controller:{controller} action:{action}");
        }
    }
}
