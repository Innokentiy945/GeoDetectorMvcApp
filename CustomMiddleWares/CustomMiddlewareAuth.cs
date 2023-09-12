using System.Text;

namespace GeoDetectorMvcApp.CustomMiddleWares
{
    public class CustomMiddlewareAuth
    {
        private readonly RequestDelegate _next;

        public CustomMiddlewareAuth(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext httpContext)
        {
            var routvalues = httpContext.GetRouteData().Values;

            if ((httpContext.Session.TryGetValue("IsLoggedIn", out var byteArr) && Encoding.UTF8.GetString(byteArr, 0, byteArr.Length) == "True")
                || (routvalues["controller"]?.ToString() == "Account" && routvalues["action"]?.ToString() == "Login"))
            {
                await _next.Invoke(httpContext);
            }
            else
            {
                httpContext.Response.Redirect("/account/login");
            }

        }
    }
}
