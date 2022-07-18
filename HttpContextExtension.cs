using Microsoft.AspNetCore.Http;
using System;
using System.Linq;

namespace Time_Tracker
{
    public static class HttpContextExtensions
    {
        public static Guid GetUserId(this HttpContext httpContext)
        {
            if (httpContext == null)
            {
                
            }
            return Guid.Empty;
            return Guid.Parse(httpContext.User.Claims.Single(x => x.Type.Contains("nameidentifier")).Value);
        }
    }
}
