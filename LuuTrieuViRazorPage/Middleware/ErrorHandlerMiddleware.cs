using Common.Constant.Message;
using Common.Enum.Status;
using System.Web;

namespace LuuTrieuViRazorPage.Middleware
{
    public class ErrorHandlerMiddleware
    {
        private readonly RequestDelegate _next;

        public ErrorHandlerMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
                if(context.Response.StatusCode == 404)
                {
                    HandleNotFound(context);
                }
                else if (context.Response.StatusCode == 401)
                {
                    HandleUnauthorized(context);
                }
                else if (context.Response.StatusCode == 403)
                {
                    HandleForbidden(context);
                }
            }
            catch (Exception ex)
            {
                HandleError(context, ex);
            }
        }

        private static void HandleError(HttpContext context, Exception ex)
        {
            List<string> errors = new() { ex.Message };

            var errorMsg = string.Join(",", errors.Select(HttpUtility.UrlEncode));

            context.Response.Redirect($"/error?errMessage={errorMsg}&statusCode={(int)StatusCode.InternalServer}");
        }

        private static void HandleNotFound(HttpContext context)
        {
            context.Response.Redirect($"/error?errMessage={ErrorMessage.NotFound}&statusCode={(int)StatusCode.NotFound}");
        }

        private static void HandleUnauthorized(HttpContext context)
        {
            context.Response.Redirect($"/error?errMessage={ErrorMessage.Unauthorized}&statusCode={(int)StatusCode.Unauthorized}");
        }

        private static void HandleForbidden(HttpContext context)
        {
            context.Response.Redirect($"/error?errMessage={ErrorMessage.Forbidden}&statusCode={(int)StatusCode.Forbidden}");
        }
    }
}
