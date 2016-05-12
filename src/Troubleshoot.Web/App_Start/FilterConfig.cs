using System.Web;
using System.Web.Mvc;

namespace Troubleshoot.Web
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new MetricMeasurementAttribute());
            filters.Add(new HandleErrorAttribute());
            filters.Add(new RequestCounterAttribute());
        }
    }
}
