using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace BookingApp.WebApi.Filters
{
    public class TimeControlFilter : ActionFilterAttribute
    {
        public string StartTime { get; set; }
        public string EndTime { get; set; }

        public override void OnActionExecuted(ActionExecutedContext context)
        {
            var now = DateTime.Now.TimeOfDay;

            StartTime = "23:00";
            EndTime = "23:59";

            if(now >= TimeSpan.Parse(StartTime) && now<= TimeSpan.Parse(EndTime))
            {
                base.OnActionExecuted(context);
            }
            else
            {
                context.Result = new ContentResult
                {
                    Content = "Bu saatler arasında bir end-pointe istek atılamaz",
                    StatusCode = 403
                };
            }

            
        }
    }
}
