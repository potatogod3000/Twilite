using System.Diagnostics;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Twilite.Data;
using Twilite.Models;

namespace Twilite.Controllers;

public class ActionsController : Controller {
    
    private readonly ApplicationDbContext _db;
    private readonly ILogger<HomeController> _logger;

    public ActionsController(ApplicationDbContext db,
        ILogger<HomeController> logger) {
        _logger = logger;
        _db = db;
    }

    public IActionResult Explore() {
        ViewData["Posts"] = _db.Posts.ToList();
        return View(ViewData);
    }

    [Authorize]
    public IActionResult Notifications() {
        return View();
    }

    [Authorize]
    public IActionResult Messages() {
        return View();
    }

    [Authorize]
    public IActionResult Communities() {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}