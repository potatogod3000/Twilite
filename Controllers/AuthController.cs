using System.Data.Common;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.CSharp.Syntax;
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

    /* [HttpPost]
    public IActionResult Login(UserInfoModel model) {        
    
        if(_db.Users()) {
            return RedirectToAction("Index", "Home");            
        }

        return View();
    } */

    public IActionResult Register() {

        return View();
    }

    [HttpPost]
    public IActionResult Register(UserInfoModel model) {

        if(model.Password != null && model.UserName == model.Password) {
            ModelState.AddModelError("password", "The User Name and the Password cannot be the same");
        }

        if(ModelState.IsValid) {
            _db.Users.Add(model);
            _db.SaveChanges();
            return RedirectToAction("Index", "Home");
        }

        return View();
    }
}