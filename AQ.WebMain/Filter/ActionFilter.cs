using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using AQ.ViewModels;
using System.Net;
using Microsoft.Extensions.Logging;
using AQ.ModelExtension;

namespace AQ.WebMain.Filter
{
    public class ActionFilter : ActionFilterAttribute
    {
        private ILogger<ActionFilter> _logger;
        public ActionFilter(ILogger<ActionFilter> logger)
        {
            _logger = logger;
        }

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            //_logger.LogDebug("Action执行前 OnActionExecuting");
            //context.Result = new ObjectResult(CommonResults.Exception);
            //context.HttpContext.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
            base.OnActionExecuting(context);
        }

        public override void OnActionExecuted(ActionExecutedContext context)
        {
            //_logger.LogDebug("Action执行后 OnActionExecuted");
            base.OnActionExecuted(context);
        }

        public override void OnResultExecuting(ResultExecutingContext context)
        {
            //_logger.LogDebug("Result执行前 OnResultExecuting");
            base.OnResultExecuting(context);
        }

        public override void OnResultExecuted(ResultExecutedContext context)
        {
            //_logger.LogDebug("Result执行后 OnResultExecuted");
            base.OnResultExecuted(context);
        }
    }
}
