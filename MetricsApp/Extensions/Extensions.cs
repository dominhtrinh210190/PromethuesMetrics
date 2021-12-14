using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using MetricsApp.Middleware;

namespace MetricsApp.Extensions
{
    public static class Extensions
    {
        public static IApplicationBuilder UseMetricsMiddleware(this IApplicationBuilder buid)
        {
            return buid.UseMiddleware<MetricsMiddleware>();
        }
    }
}
