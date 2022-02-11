using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using WebApplication1.Models;
using WebApplication3;

namespace WebApplication1.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly ISmtpEmailSender _smtpEmailSender;
    private readonly IOptions<SmtpCredentials> _options;

    public HomeController(ILogger<HomeController> logger, ISmtpEmailSender smtpEmailSender, IOptions<SmtpCredentials> options)
    {
        _logger = logger;
        _smtpEmailSender = smtpEmailSender;
        _options = options;
    }

    public IActionResult Index()
    {
        return View();
    }

    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel {RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier});
    }
    
    public IActionResult SetupCredentials()
    {
        if (HttpContext.Request.Method == "POST")
        {
            var userName = HttpContext.Request.Form["userName"].ToString();
            var host = HttpContext.Request.Form["host"].ToString();
            int port = int.Parse(HttpContext.Request.Form["port"].ToString());
            var password = HttpContext.Request.Form["password"].ToString();
            
            _smtpEmailSender.Setup(host, userName, password, port);
        }
        return View(_options.Value);
    }
}