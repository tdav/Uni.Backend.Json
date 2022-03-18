using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Prometheus;

namespace App.Extensions
{
    /// <summary>
    /// https://prometheus.io/  Power your metrics and alerting with a leading open-source monitoring solution.
    /// https://grafana.com/    The analytics platform for all your metrics
    /// </summary>
    public static class PrometheusMetricService
    {
        public static void AddMyMetricServer(this IServiceCollection services)
        {
            // prometheus-net - /metrics
           // services.AddSingleton<MetricReporter>();
            services.AddHealthChecks().ForwardToPrometheus();

        }

        public static void UseMyMetricServer(this IApplicationBuilder app)
        {
            // prometheus-net - /metrics
            app.UseMetricServer();
            app.UseHttpMetrics();
           // app.UseMiddleware<ResponseMetricMiddleware>();

        }
    }
}
