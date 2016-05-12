using System;
using System.Web;

namespace Troubleshoot.Web.Models
{
    public class BaseModel
    {
        private double? _timeElapsedMilisecond;
        public double TimeElapsedMilisecond
        {
            get
            {
                double i = -1;
                if (!_timeElapsedMilisecond.HasValue)
                {
                    var timeElapsed =
                        HttpContext.Current.Items[MetricMeasurementAttribute.TimeElapsedMilisecondContextItemKey]
                            .ToString();
                    if (string.IsNullOrEmpty(timeElapsed)) timeElapsed = "-1";
                    _timeElapsedMilisecond = Convert.ToDouble(timeElapsed);
                    
                }
                return _timeElapsedMilisecond.Value; ;
            }
        }
    }
}