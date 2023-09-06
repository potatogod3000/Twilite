using System.Dynamic;
using Microsoft.AspNetCore.Mvc;
using Twilite.Data;
using Twilite.Models;

namespace Twilite.ViewComponents;

public class PostsViewComponent : ViewComponent {

    private readonly ApplicationDbContext _db;

    public PostsViewComponent(ApplicationDbContext db) {
        _db = db;
    }

    public IViewComponentResult Invoke() {
        ViewData["PostInfoObj"] = _db.Posts.ToList();
        ViewData["UserProfiles"] = _db.UserProfiles.ToList();
        
        return View("ShowPosts", ViewData);
    }
}