using Microsoft.AspNetCore.Mvc;
using Twilite.Data;
using Twilite.Models;

namespace Twilite.ViewComponents;

public class UserActionsViewComponent : ViewComponent {

    private readonly ApplicationDbContext _db;

    public UserActionsViewComponent(ApplicationDbContext db) {
        _db = db;
    }

    [HttpGet]
    public IViewComponentResult Invoke(PostInfoModel obj) {
        ViewData["CurrentUserProfile"] = _db.UserProfiles.FirstOrDefault(x => x.UserName == User.Identity.Name);
        ViewData["CurrentPostModel"] = obj;
        
        return View("UserActions", ViewData);
    }
}