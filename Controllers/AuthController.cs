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

        if(model.UserName.Contains(' ')) {
            ModelState.AddModelError("", $"The User Name - \'{model.UserName}\' is invalid. Username cannot contain a Space.");
        }

        return View();
    } */

    public IActionResult Register() {

        return View();
    }

    [HttpPost]
    public IActionResult Register(UserInfoModel model) {

        List<string> invalidUserNames = new()
            {"test", "test_user", "test.user", "test-user", "admin", "twilite", "twiliteadmin", "twilite-admin", "twilite.admin", "twilite_admin"};

        if(model.Password != null && model.UserName == model.Password) {
            ModelState.AddModelError("password", "The User Name and the Password cannot be the same.");
        }

        for(int i = 0; i < invalidUserNames.Count; i++) {
            if(model.UserName == "" && model.UserName.ToLower() == invalidUserNames[i]) {
                ModelState.AddModelError("", $"The User Name - \'{model.UserName}\' is invalid. Username cannot contain \"Twilite\", \"Admin\" or \"Test\".");
            }
        }

        if(model.UserName != null && model.UserName.Contains(' ')) {
            ModelState.AddModelError("username", $"User Name cannot contain a Space.");
        }

        if(ModelState.IsValid) {
            _db.Users.Add(model);
            _db.SaveChanges();
            return RedirectToAction("Index", "Home");
        }

        return View();
    }
}