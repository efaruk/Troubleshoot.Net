using System.Web.Mvc;
using Glimpse.Core.Extensibility;
using Glimpse.Mvc.AlternateType;
using Troubleshoot.Common;

namespace Troubleshoot.Web
{
    public class RequestCounterAttribute: ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            Trouble.CounterIncrement();
            base.OnActionExecuting(filterContext);
        }
    }
}