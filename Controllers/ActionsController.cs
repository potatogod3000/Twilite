using System.Diagnostics;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Twilite.Data;
using Twilite.Models;

namespace Twilite.Controllers;

public class ActionsController : Controller
{
    /* private readonly ILogger<ActionsController> _logger;
    public ActionsController(ILogger<ActionsController> logger)
    {
        _logger = logger;
    } */

    private readonly ApplicationDbContext _db;

    public ActionsController(ApplicationDbContext db) {
        _db = db;
    }

    public IActionResult Messages() {
        return View();
    }

    public IActionResult Notifications() {
        return View();
    }

    public IActionResult Explore() {
        List<PostInfoModel> PostsListObj = _db.Posts.ToList();
        return View(PostsListObj);
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
