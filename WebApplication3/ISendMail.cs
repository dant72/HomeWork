using System.Net.Mail;

namespace WebApplication3;

public interface ISendMail
{
    public void Setup(string host, string userName, string password);
    public async Task<bool> Send(string sendTo, string? subject, string? body, CancellationToken stoppingToken);
}