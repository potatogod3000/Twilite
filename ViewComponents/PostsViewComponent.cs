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
        List<PostInfoModel> PostInfoObj = _db.Posts.ToList();
        
        return View("ShowPosts", PostInfoObj);
    }
}