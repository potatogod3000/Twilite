using System.Diagnostics;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Twilite.Models;

namespace Twilite.Controllers;

public class ActionsController : Controller
{
    private readonly ILogger<ActionsController> _logger;

    public ActionsController(ILogger<ActionsController> logger)
    {
        _logger = logger;
    }

    public IActionResult Messages()
    {
        return View();
    }

    public IActionResult Notifications()
    {
        return View();
    }

    public IActionResult Explore()
    {
        return View();
    }

    public IActionResult Lists()
    {
        return View();
    }

    public IActionResult Bookmarks()
    {
        return View();
    }

    public IActionResult Communities()
    {
        return View();
    }

    // [Authorize]
    public IActionResult Profile()
    {
        return View();
    }

    public IActionResult More()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
