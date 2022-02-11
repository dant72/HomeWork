using System.Diagnostics;
using HttpModels;

namespace WebApplication3;

public class BackService
{
    public class ExampleBackgroundService : BackgroundService
    {
        private readonly ISmtpEmailSender _smtpEmailSender;
        private readonly IClock _time;
        private readonly ILogger<BackService> _logger;

        public ExampleBackgroundService(ILogger<BackService> logger, ISmtpEmailSender smtpEmailSender, IClock time)
        {
            _smtpEmailSender = smtpEmailSender;
            _time = time;
            _logger = logger;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            using var timer = new PeriodicTimer(TimeSpan.FromMinutes(1));
            var sw = Stopwatch.StartNew();
            while (await timer.WaitForNextTickAsync(stoppingToken))
            {
                
                bool res = await _smtpEmailSender.Send("Kuty-y4vy@yandex.ru","test service",$"{_time.LocalTime}({_time.TimeZone.DisplayName}) - Сервер работает уже {sw.Elapsed}, ({GC.GetTotalAllocatedBytes()/1024 / 1024} MB)", stoppingToken);
                _logger.LogWarning($"Сервер работает уже {sw.Elapsed} {(res ? "Сообщение успешно доставлено" : "Сообщение не доставлено")}");
            }
        }
    }
}