namespace Microsoft.AspNetCore.Http
{
    public static class HttpContextExtensions
    {
        public static string GetRequestId(this HttpContext httpContext) => httpContext?.Request.Headers["x-requestId"];
    }
}
