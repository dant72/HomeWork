using Microsoft.AspNetCore.Mvc.Filters;
using System.Diagnostics;

namespace HttpApiServer_backend.Controllers.Filters
{
    public class TimeFilter : IActionFilter
    {
        private readonly Stopwatch _stopWatch;
        private readonly ILogger<TimeFilter> _logger;
        public TimeFilter(ILogger<TimeFilter> logger)
        {
            _logger = logger;
            _stopWatch = new Stopwatch();
        }
        public void OnActionExecuted(ActionExecutedContext context)
        {
            _stopWatch.Stop();

            TimeSpan ts = _stopWatch.Elapsed;
            var action = context.HttpContext.Request.Path;
            var httpMethod = context.HttpContext.Request.Method;
            var delta = string.Format("{0:00}:{1:00}:{2:00}.{3:0000}", ts.Hours, ts.Minutes, ts.Seconds, ts.Milliseconds);
            _logger.LogInformation($"{httpMethod}: {action}; Runtime: {delta}");
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
            _stopWatch.Start();
        }
    }
}
