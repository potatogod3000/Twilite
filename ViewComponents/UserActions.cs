using Microsoft.AspNetCore.Mvc;
using Twilite.Data;
using Twilite.Models;

namespace Twilite.ViewComponents;

public class UserActions : ViewComponent {

    private readonly ApplicationDbContext _db;

    public UserActions(ApplicationDbContext db) {
        _db = db;
    }

    [HttpGet]
    public IViewComponentResult Invoke(PostInfoModel obj) {
        ViewData["CurrentUserProfile"] = _db.UserProfiles.FirstOrDefault(x => x.UserName == User.Identity.Name);
        ViewData["CurrentPostModel"] = obj;
        
        return View("UserActions", ViewData);
    }
}