using System.Diagnostics;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Twilite.Data;
using Twilite.Models;

namespace Twilite.Controllers;

public class ActionsController : Controller
{
    private readonly ILogger<ActionsController> _logger;
 
    public ActionsController(ILogger<ActionsController> logger)
    {
        _logger = logger;
    }

    public IActionResult Explore() {

        return View();
    }

    public IActionResult Notifications() {
        return View();
    }

    public IActionResult Messages() {
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

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
