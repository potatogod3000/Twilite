using Twilite.Models;
using Twilite.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

namespace Twilite.Controllers;

public class PostController : Controller {
    
    private readonly ApplicationDbContext _db;
    
    public PostController(ApplicationDbContext db) {
        _db = db;
    }

    [Authorize]
    public IActionResult EditPost(int? PostId) {
        if(PostId == null || PostId == 0) {
            
            return RedirectToAction("Explore", "Actions");
        }
        
        PostInfoModel? PostInfoObj = _db.Posts.FirstOrDefault(id=>id.PostId == PostId);
        if(PostInfoObj == null) {

            return RedirectToAction("Explore", "Actions");
        }

        return View(PostInfoObj);
    }

    /* [Authorize]
    [HttpPost]
    public IActionResult EditPost(PostInfoModel Post) {
        if(Post.UserName == User.Identity.Name) {
            _db.Posts.Update(Post);
            _db.SaveChanges();
            return RedirectToAction("Explore", "Actions");
        }
        

        return View();
    } */

    [Authorize]
    [HttpPost]
    public IActionResult CreatePost(PostInfoModel Post) {

        if(User.Identity.IsAuthenticated) {
            Post.UserName = User.Identity.Name;
            /* PostInfoModel PostInfoObj = new PostInfoModel() {
                PostInfoObj.UserName = User.Identity.Name;
                }; */
        
            _db.Posts.Update(Post);
            _db.SaveChanges();
            return RedirectToAction("Explore", "Actions");
            
        }
        
        return RedirectToAction("Index", "Home");
    }
}