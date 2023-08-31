using Microsoft.AspNetCore.Mvc;
using Twilite.Data;
using Twilite.Models;

namespace Twilite.ViewComponents;

public class UserViewComponent : ViewComponent {

    private readonly ApplicationDbContext _db;
    
    public UserViewComponent(ApplicationDbContext db) {
        _db = db;
    }

    public IViewComponentResult Invoke() {
        List<UserInfoModel> UserInfoObj = _db.Users.ToList();

        return View("UserPage", UserInfoObj);
    }
}