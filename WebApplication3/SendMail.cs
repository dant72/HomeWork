using System.Net;
using Microsoft.Extensions.Options;

namespace WebApplication3;
using System.Net.Mail;

public class SendMail : ISendMail
{
    private string userName;
    private SmtpClient SmtpClient { set; get; }
    private SmtpCredentials _smtpCredentials;

    public SendMail(IOptions<SmtpCredentials> options)
    {
        _smtpCredentials = options.Value;
        userName = _smtpCredentials.UserName;
        SmtpClient =  new SmtpClient(_smtpCredentials.Host)
        {
            Port = 25,
            Credentials = new NetworkCredential(userName, _smtpCredentials.Password),
            EnableSsl = true
        };
    }

    public void Setup(string host, string userName, string password)
    {
        this.userName = userName;
        SmtpClient = new SmtpClient(host)
        {
            Port = 25,
            Credentials = new NetworkCredential(userName, password),
            EnableSsl = true
        };
    }

    public async Task<bool> Send(string sendTo, string? subject, string? body, CancellationToken stoppingToken)
    {
        //using
        try
        {
            await SmtpClient.SendMailAsync(userName,sendTo, subject, body, stoppingToken);
        }
        catch (Exception e)
        {
            //Console.WriteLine(e);
            
            return false;
        }


        return true;
    }
 
}