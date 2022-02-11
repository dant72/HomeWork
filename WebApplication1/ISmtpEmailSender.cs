using System.Net.Mail;

namespace WebApplication3;

public interface ISmtpEmailSender
{
    public void Setup(string host, string userName, string password, int port);
    public Task Send(string sendTo, string? subject, string? body, CancellationToken stoppingToken);
}