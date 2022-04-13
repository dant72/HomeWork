using System.Net;
using Microsoft.Extensions.Options;

namespace WebApplication3;
using System.Net.Mail;

public class SmtpEmailSender : ISmtpEmailSender
{
    private string userName;
    private SmtpClient SmtpClient { set; get; }
    private SmtpCredentials _smtpCredentials;
    private ILogger<SmtpEmailSender> _logger;


    public SmtpEmailSender(IOptionsSnapshot<SmtpCredentials> options, ILogger<SmtpEmailSender> logger)
    {
        _logger = logger;
        _smtpCredentials = options.Value;
        userName = _smtpCredentials.UserName;
        SmtpClient =  new SmtpClient(_smtpCredentials.Host)
        {
            Port = _smtpCredentials.Port,
            Credentials = new NetworkCredential(userName, _smtpCredentials.Password),
            EnableSsl = true
        };
    }

    public void Setup(string host, string userName, string password, int port = 25)
    {
        this.userName = userName;
        SmtpClient = new SmtpClient(host)
        {
            Port = port,
            Credentials = new NetworkCredential(userName, password),
            EnableSsl = true
        };
    }

    public async Task Send(string sendTo, string? subject, string? body, CancellationToken stoppingToken)
    {
        try
        {
            await SmtpClient.SendMailAsync(userName, sendTo, subject, body, stoppingToken);
            _logger.LogInformation($"Message send to {sendTo}\nSubject: {subject}\nBody: {body}");
        }
        catch(Exception ex)
        {
            _logger.LogWarning(ex.Message);
        }
    }
 
}