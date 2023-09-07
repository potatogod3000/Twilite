using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
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
    public IActionResult CurrentUserProfile() {
        ViewData["CurrentUserProfile"] = _db.UserProfiles.FirstOrDefault(x => x.UserName == User.Identity.Name);
        ViewData["CurrentUserPosts"] = _db.Posts.Where(x => x.UserName == User.Identity.Name).ToList();

        return View("CurrentUserProfile", ViewData);
    }
}