using System.Diagnostics;
using HttpModels;

namespace WebApplication3;

public class BackService
{
    public class ExampleBackgroundService : BackgroundService
    {
        private readonly ISendMail _sendMail;
        private readonly IMyTime _time;

        public ExampleBackgroundService(ISendMail sendMail, IMyTime time)
        {
            _sendMail = sendMail;
            _time = time;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            Process proc = Process.GetCurrentProcess();
            using var timer = new PeriodicTimer(TimeSpan.FromMinutes(1));
            var sw = Stopwatch.StartNew();
            while (await timer.WaitForNextTickAsync(stoppingToken))
            {
                Console.WriteLine($"Сервер работает уже {sw.Elapsed}");
                //_logger.LogInformation("Сервер работает уже {WorkTime}", sw.Elapsed);
                _sendMail.Send("Kuty-y4vy@yandex.ru","test service",$"{_time.LocalTime}({_time.TimeZone.DisplayName}) - Сервер работает уже {sw.Elapsed}, ({proc.PrivateMemorySize64 / 1024 / 1024} MB)");
            }
        }
    }
}