using System;
using System.Web.Mvc;

namespace Troubleshoot.Web
{
    public class MetricMeasurementAttribute: ActionFilterAttribute
    {
        private const string RequestBeginContextItemKey = "RequestStarted";
        public const string TimeElapsedMilisecondContextItemKey = "TimeElapsedMilisecond";
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            filterContext.HttpContext.Items[RequestBeginContextItemKey] = DateTime.UtcNow;
            base.OnActionExecuting(filterContext);
        }

        public override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            base.OnActionExecuted(filterContext);
            DateTime? requestStarted = filterContext.HttpContext.Items[RequestBeginContextItemKey] as DateTime?;
            if (!requestStarted.HasValue) throw new ArgumentNullException(RequestBeginContextItemKey);
            var difference = DateTime.UtcNow.Subtract(requestStarted.Value);
            filterContext.HttpContext.Items[TimeElapsedMilisecondContextItemKey] = difference.TotalMilliseconds;
        }
    }
}