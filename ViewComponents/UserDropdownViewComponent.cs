using System.Data.Common;
using Microsoft.AspNetCore.Mvc;
using Twilite.Data;
using Twilite.Models;

namespace Twilite.ViewComponents;

public class UserDropdownViewComponent : ViewComponent {

    private readonly ApplicationDbContext _db;
    public UserDropdownViewComponent(ApplicationDbContext db) {
        _db = db;
    }

    public IViewComponentResult Invoke() {
        UserProfileModel UserProfile = _db.UserProfiles.FirstOrDefault(x => x.UserName == User.Identity.Name);
        return View("UserDropdown", UserProfile);
    }
}