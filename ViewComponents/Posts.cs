using System.Dynamic;
using Microsoft.AspNetCore.Mvc;
using Twilite.Data;
using Twilite.Models;

namespace Twilite.ViewComponents;

public class Posts : ViewComponent {

    private readonly ApplicationDbContext _db;

    public Posts(ApplicationDbContext db) {
        _db = db;
    }

    public IViewComponentResult Invoke(PostInfoModel Post) {
        ViewData["Post"] = Post;
        
        return View("ShowPosts", ViewData);
    }
}