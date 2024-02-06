using System.Net;

namespace MELC.WebApp.MVC.Extensions
{
    public class AuthorizeMiddleware
    {
        public readonly RequestDelegate _next;

        public AuthorizeMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext httpContext)
        {
            try
            {
                await _next(httpContext);
                ValidateStatus(httpContext);
            }
            catch (CustomHttpRequestException ex)
            {
                HandleRequestExceptionAsync(httpContext, ex);
            }
        }

        private static void HandleRequestExceptionAsync(HttpContext context, CustomHttpRequestException httpRequestException)
        {
            if (httpRequestException.StatusCode == HttpStatusCode.Unauthorized)
            {
                context.Response.StatusCode = (int)HttpStatusCode.Unauthorized;
                context.Response.Redirect($"/login?ReturnUrl={context.Request.Path}");
                return;
            }

            if (httpRequestException.StatusCode == HttpStatusCode.Forbidden)
            {
                context.Response.StatusCode = (int)HttpStatusCode.Forbidden;
                context.Response.Redirect($"/erro/{(int)HttpStatusCode.Forbidden}");
                return;
            }

            if (httpRequestException.StatusCode == HttpStatusCode.InternalServerError)
            {
                context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                context.Response.Redirect($"/erro/{(int)HttpStatusCode.InternalServerError}");
                return;
            }

            context.Response.StatusCode = (int)httpRequestException.StatusCode;
        }

        private static void ValidateStatus(HttpContext context)
        {
            if (context.Response.StatusCode == (int)HttpStatusCode.Unauthorized)
            {
                context.Response.Redirect($"/login?ReturnUrl={context.Request.Path}");
                return;
            }

            if (context.Response.StatusCode == (int)HttpStatusCode.Forbidden)
            {
                context.Response.Redirect($"/erro/{(int)HttpStatusCode.Forbidden}");
                return;
            }

            if (context.Response.StatusCode == (int)HttpStatusCode.InternalServerError)
            {
                context.Response.Redirect($"/erro/{(int)HttpStatusCode.InternalServerError}");
                return;
            }
        }
    }
}
