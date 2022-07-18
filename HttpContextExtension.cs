using Microsoft.AspNetCore.Http;
using System;
using System.Linq;

namespace Time_Tracker
{
    public static class HttpContextExtensions
    {
        public static string GetUserId(this HttpContext httpContext)
        {
            if (httpContext == null)
            {
                return Convert.ToString(Guid.Empty);
            }
            
            return httpContext.User.Claims.Single(x => x.Type.Contains("nameidentifier")).Value;
        }
    }
}
