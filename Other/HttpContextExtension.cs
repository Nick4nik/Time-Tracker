using Microsoft.AspNetCore.Http;
using System;
using System.Linq;

namespace Time_Tracker.Other
{
    public static class HttpContextExtension
    {
        public static string GetUserIdString(this HttpContext httpContext)
        {
            if (httpContext == null)
            {
                return Convert.ToString(Guid.Empty);
            }

            return httpContext.User.Claims.Single(x => x.Type.Contains("nameidentifier")).Value;
        }

        public static Guid GetUserIdGuid(this HttpContext httpContext)
        {
            if (httpContext == null)
            {
                return Guid.Empty;
            }

            return Guid.Parse(httpContext.User.Claims.Single(x => x.Type.Contains("nameidentifier")).Value);
        }
    }
}
