using BookingApp.Business.Operations.Setting;

namespace BookingApp.WebApi.Middlewares
{
    public class MaintenenceMiddleware
    {
        private readonly RequestDelegate _next;
        

        public MaintenenceMiddleware(RequestDelegate next)
        {
            _next = next;
            
        }

        public async Task Invoke(HttpContext context)
        {
            var settingService = context.RequestServices.GetRequiredService<ISettingService>();

            bool mainteneneceMode = settingService.GetMaintenanceState();

            if(context.Request.Path.StartsWithSegments("/api/auth/login") || context.Request.Path.StartsWithSegments("/api/settings"))
            {
                await _next(context);
                return;
            }


            if (mainteneneceMode)
            {
                await context.Response.WriteAsync("Şu anda hizmet verememekteyiz");
            }

            else
            {
                await _next(context);
            }




        }




    }
}
