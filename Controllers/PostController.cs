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
    public IActionResult CreatePost() {
        return View();
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

    [Authorize]
    [HttpGet]
    public IActionResult AddFollower(string PostUserName, string CurrentUserName) {
        // Add Following info to Post's User
        UserProfileModel PostUserProfile = _db.UserProfiles.FirstOrDefault(x => x.UserName == PostUserName);
        
        if(PostUserProfile == null) {
            return BadRequest();
        }

        if(PostUserProfile.UserName != CurrentUserName && !PostUserProfile.Followers.Contains(CurrentUserName)) {
            PostUserProfile.Followers.Add(CurrentUserName);

            if(ModelState.IsValid) {
                _db.Update(PostUserProfile);
                _db.SaveChanges();
            }
        }

        // Add Follower info to Post's User
        UserProfileModel CurrentUserProfile = _db.UserProfiles.FirstOrDefault(x => x.UserName == CurrentUserName);

        if(CurrentUserProfile == null) {
            return BadRequest();
        }

        if(CurrentUserProfile.UserName != PostUserName && !CurrentUserProfile.Following.Contains(PostUserName)) {
            CurrentUserProfile.Following.Add(PostUserName);

            if(ModelState.IsValid) {
                _db.Update(CurrentUserProfile);
                _db.SaveChanges();
            }
        }

        return RedirectToAction("Explore", "Actions");
    }

    [Authorize]
    [HttpGet]
    public IActionResult RemoveFollower(string PostUserName, string CurrentUserName) {
        // Remove Following info from Post's User
        UserProfileModel PostUserProfile = _db.UserProfiles.FirstOrDefault(x => x.UserName == PostUserName);
        
        if(PostUserProfile.Followers.Contains(CurrentUserName)) {
            PostUserProfile.Followers.Remove(CurrentUserName);
        }
        
        if(ModelState.IsValid) {
                _db.Update(PostUserProfile);
                _db.SaveChanges();
        }

        // Remove Follower info from Post's User
        UserProfileModel CurrentUserProfile = _db.UserProfiles.FirstOrDefault(x => x.UserName == CurrentUserName);
        
        if(CurrentUserProfile.Following.Contains(PostUserName)) {
            CurrentUserProfile.Following.Remove(PostUserName);
        }
        
        if(ModelState.IsValid) {
                _db.Update(CurrentUserProfile);
                _db.SaveChanges();
        }
        
        return RedirectToAction("Explore", "Actions");
    }

}