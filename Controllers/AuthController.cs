using Microsoft.AspNetCore.Mvc;
using Twilite.Data;
using Twilite.Models;

namespace Twilite.Controllers;

public class AuthController : Controller {
    /* private readonly ILogger<HomeController> _logger;
    public HomeController(ILogger<HomeController> logger)
    {
        _logger = logger;
    } */
    
    private readonly ApplicationDbContext _db;

    public AuthController(ApplicationDbContext db) {
        _db = db;
    }

    public IActionResult Login() {
        return View();
    }

    public IActionResult Register() {

        return View();
    }
}