using System.Data.Common;
using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Twilite.Models;
using Twilite.Data;

namespace Twilite.Controllers;

public class HomeController : Controller {
    
    private readonly ApplicationDbContext _db;
    private ILogger<HomeController> _logger;

    public HomeController(ApplicationDbContext db,
        ILogger<HomeController> logger) {
        _logger = logger;
        _db = db;
    }

    public IActionResult Index() {
        ViewData["Posts"] = _db.Posts.ToList();
        return View(ViewData);
    }

    public IActionResult Privacy() {
        return View();
    }

    public IActionResult About() {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
