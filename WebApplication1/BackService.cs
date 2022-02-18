using System.Diagnostics;
using HttpModels;
using Polly;
using Polly.Retry;

namespace WebApplication3;

public class BackService
{
    public class ExampleBackgroundService : BackgroundService
    {
        //private readonly ISmtpEmailSender _smtpEmailSender;
        private readonly IClock _time;
        private readonly ILogger<BackService> _logger;
        private readonly IServiceProvider _locator;

        public ExampleBackgroundService(ILogger<BackService> logger, IServiceProvider locator, IClock? time)
        {
           // _smtpEmailSender = smtpEmailSender;
            _time = time;
            _logger = logger;
            _locator = locator;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _logger.LogInformation("Сервис запущен");
           // using (ProviderAliasAttribute.)
           using (var scope = _locator.CreateScope())
           {
               var srv = scope.ServiceProvider.GetRequiredService<ISmtpEmailSender>();

               var timer = new PeriodicTimer(TimeSpan.FromMinutes(1));
               var sw = Stopwatch.StartNew();
               while (await timer.WaitForNextTickAsync(stoppingToken))
               {
                   AsyncRetryPolicy? policy = Policy
                       .Handle<Exception>()
                       .RetryAsync(2, onRetry: (exception, retryAttempt) =>
                           {
                               _logger.LogWarning(
                                   exception, "Error while sending email. Retrying: {Attempt}", retryAttempt);
                           });

                   PolicyResult? result = await policy.ExecuteAndCaptureAsync(
                           token => Send(token, srv, sw), stoppingToken);
                   
                   if (result.Outcome == OutcomeType.Failure)
                   {
                       _logger.LogError(result.FinalException, "There was an error while sending email");
                   }
               }
           }
        }

        private Task Send(CancellationToken stoppingToken, ISmtpEmailSender srv, Stopwatch sw)
        {
            return srv.Send("Kuty-y4vy@yandex.ru", "test service",
                $"{_time.LocalTime}({_time.TimeZone.DisplayName}) - Сервер работает уже {sw.Elapsed}, ({GC.GetTotalAllocatedBytes() / 1024 / 1024} MB)",
                stoppingToken);
        }
    }
}