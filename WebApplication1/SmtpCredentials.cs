using Microsoft.Extensions.Options;

namespace WebApplication3;

public class SmtpCredentials
{
    public string Host { get; set; }
    public string UserName { get; set; }
    public string Password { get; set; }
    
    public int Port { get; set; }
}