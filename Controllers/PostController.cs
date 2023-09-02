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
    [HttpPost]
    public IActionResult CreatePost(PostInfoModel Post) {
        Post.UserName = User.Identity.Name;

        if(Post.PostContent == null) {
            ModelState.AddModelError("", "You haven't entered anything in the input area.");
        }
        else {
            _db.Posts.Update(Post);
            _db.SaveChanges();
            return RedirectToAction("Explore", "Actions");        
        }
        
        return RedirectToAction("Index", "Home");
    }


    [Authorize]
    [HttpGet]
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

    [Authorize]
    [HttpPost]
    public IActionResult EditPost(PostInfoModel Post) {
        int? PostId = Post.PostId;
        PostInfoModel? PostInfoObj = _db.Posts.FirstOrDefault(id=>id.PostId == PostId);

        if(PostInfoObj.PostContent == Post.PostContent) {
            ModelState.AddModelError("", "You haven't made any changes to this post.");
        }

        if(Post.UserName == User.Identity.Name && ModelState.IsValid) {
            // Clear all previous tracking of the ApplicationDbContext _db
            _db.ChangeTracker.Clear();
            
            _db.Posts.Update(Post);
            _db.SaveChanges();
            return RedirectToAction("Explore", "Actions");
        }   

        return View(PostInfoObj);
    }

    [HttpPost]
    [Authorize]
    public IActionResult DeletePost(PostInfoModel Post) {
        
        if(User.Identity.Name == Post.UserName) {
            _db.Posts.Remove(Post);
            _db.SaveChanges();
            return RedirectToAction("Explore", "Actions");
        }
        return RedirectToAction("Explore", "Actions");
    }

    [HttpGet]
    [Authorize]
    public IActionResult DeletePost(int? PostId) {

        if(PostId == null || PostId == 0) {
            return RedirectToAction("Explore", "Actions");
        }
        
        PostInfoModel? PostInfoObj = _db.Posts.FirstOrDefault(id=>id.PostId == PostId);

        if(PostInfoObj == null) {
            return RedirectToAction("Explore", "Actions");
        }

        return View(PostInfoObj);
    }

}