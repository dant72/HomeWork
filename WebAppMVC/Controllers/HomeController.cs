using System.Diagnostics;
using HttpModels;
using Microsoft.AspNetCore.Mvc;
using WebAppMVC.Models;

namespace WebAppMVC.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly IMyTime _time;

    public HomeController(ILogger<HomeController> logger, IMyTime time)
    {
        _logger = logger;
        _time = time;
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

    public IActionResult GetTime()
    {
        if (HttpContext.Request.Method == "POST")
        {
            var timeZone = HttpContext.Request.Form["timeZone"].ToString();
            _time.TimeZone = TimeZoneInfo.FindSystemTimeZoneById(timeZone);
        }
        return View(_time);
    }
}