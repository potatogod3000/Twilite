using System.Data.Common;
using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Twilite.Data;
using Twilite.Models;

namespace Twilite.Controllers;

public class HomeController : Controller {
    
    private readonly ApplicationDbContext _db;
    
    /*private readonly ILogger<HomeController>? _logger;
    public HomeController(ILogger<HomeController> logger)
    {
        _logger = logger;
    } */

    public HomeController(ApplicationDbContext db) {
        _db = db;
    }

    public IActionResult Index() {

        return View();
    }

    /* [HttpPost]
    public IActionResult Index(PostInfoModel model) {
        _db.Posts.Add(model);
        _db.SaveChanges();
        List<PostInfoModel> PostsListObj = _db.Posts.ToList();

        return View(PostsListObj);
    } */

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
