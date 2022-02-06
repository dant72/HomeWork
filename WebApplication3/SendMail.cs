using System.Net;

namespace WebApplication3;
using System.Net.Mail;

public class SendMail : ISendMail
{
    private string userName;
    private SmtpClient SmtpClient { set; get; }

    public SendMail()
    {
        userName = "asp2022@rodion-m.ru";
        SmtpClient =  new SmtpClient("smtp.beget.com")
        {
            Port = 25,
            Credentials = new NetworkCredential(userName, "aHGnOlz7"),
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

    public void Send(string sendTo, string? subject, string? body)
    {
        SmtpClient.SendAsync(userName,sendTo, subject, body, null);
    }
 
}