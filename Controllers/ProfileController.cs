using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Twilite.Data;
using Twilite.Models;

namespace Twilite.Controllers;

public class ProfileController : Controller {
    
    private readonly ApplicationDbContext _db;

    public ProfileController(ApplicationDbContext db) {
        _db = db;
    }

    [Authorize]
    [HttpGet]
    public IActionResult UserProfile(string UserName) {
        if(UserName == null) {
            ViewData["CurrentUserProfile"] = _db.UserProfiles.FirstOrDefault(x => x.UserName == User.Identity.Name);
            ViewData["CurrentUserPosts"] = _db.Posts.Where(x => x.UserName == User.Identity.Name).Include(r => r.Replies).ToList();
        }
        else {
            ViewData["CurrentUserProfile"] = _db.UserProfiles.FirstOrDefault(x => x.UserName == UserName);
            ViewData["CurrentUserPosts"] = _db.Posts.Where(x => x.UserName == UserName).Include(r => r.Replies).ToList();
        }

        return View("UserProfile", ViewData);
    }
}