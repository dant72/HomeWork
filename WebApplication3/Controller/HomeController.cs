using Microsoft.AspNetCore.Mvc;

namespace WebApplication3.Controller;

public class HomeController:Microsoft.AspNetCore.Mvc.Controller
{
    private readonly ISmtpEmailSender _smtpEmailSender;
    public HomeController(ISmtpEmailSender smtpEmailSender)
    {
        _smtpEmailSender = smtpEmailSender;
    }
    
    public IActionResult Index()
    {
        return View();
    }
}