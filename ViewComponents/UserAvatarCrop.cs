using Microsoft.AspNetCore.Mvc;
using Twilite.Data;

namespace Twilite.ViewComponents;

public class UserAvatarCrop : ViewComponent {
    
    private readonly ApplicationDbContext _db;
    public UserAvatarCrop(ApplicationDbContext db) {
        _db = db;
    }

    public IViewComponentResult Invoke() {
        ViewData["CurrentUserProfile"] = _db.UserProfiles.FirstOrDefault(x => x.UserName == User.Identity.Name);

        return View("UserAvatarCrop", ViewData);
    }
}