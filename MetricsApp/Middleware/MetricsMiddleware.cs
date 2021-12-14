using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Prometheus;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;


namespace MetricsApp.Middleware
{
    public class MetricsMiddleware
    {
        private readonly RequestDelegate next;
        private static readonly Counter TickTock = Metrics.CreateCounter("count", "Just keeps on ticking");
        private static readonly Gauge JobsInQueue = Metrics.CreateGauge("myapp_jobs_queued", "Number of jobs waiting for processing in the queue.");
        private static readonly Summary RequestSizeSummary = Metrics.CreateSummary("myapp_request_size_bytes", "Summary of request sizes (in bytes) over last 10 minutes.");
        private static readonly Histogram OrderValueHistogram = Metrics.CreateHistogram("myapp_order_value_usd", "Histogram of received order values (in USD).",
        new HistogramConfiguration
        {
            // We divide measurements in 10 buckets of $100 each, up to $1000.
            Buckets = Histogram.LinearBuckets(start: 100, width: 100, count: 10)
        });

        public MetricsMiddleware(RequestDelegate next)
        {
            this.next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            string[] subsystem = new string[1];
            var path = context.Request.Path;

            if (path.StartsWithSegments("/metrics"))
            {
                await next(context);
                return;
            }

            TickTock.Inc();
            JobsInQueue.Inc();
            OrderValueHistogram.NewTimer();
            RequestSizeSummary.NewTimer();

            try
            {
                await next(context);
            }
            catch (Exception)
            {

            }
        }
    }
}
