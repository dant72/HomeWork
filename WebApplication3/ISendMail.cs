using System.Net.Mail;

namespace WebApplication3;

public interface ISendMail
{
    public void Setup(string host, string userName, string password);
    public void Send(string sendTo, string? subject, string? body);
}