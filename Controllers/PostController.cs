using Twilite.Models;
using Twilite.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using System.Text.Encodings.Web;
using Microsoft.Extensions.WebEncoders.Testing;
using System.Net;

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

            TempData["PostMessage"] = "Your post is now online!";
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

            TempData["PostMessage"] = "Post updated";
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

        TempData["PostMessage"] = "Post deleted";
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

        TempData["FollowMessage"] = $"You followed {PostUserName}";
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

        TempData["FollowMessage"] = $"You unfollowed {PostUserName}";
        return RedirectToAction("Explore", "Actions");
    }

    [Authorize]
    [HttpPost]
    public IActionResult LikePost(int PostId) {
        PostInfoModel CurrentPost = _db.Posts.FirstOrDefault(x => x.PostId == PostId);
        UserProfileModel CurrentUser =_db.UserProfiles.FirstOrDefault(x => x.UserName == User.Identity.Name);
        
        if(CurrentPost != null && CurrentUser != null && CurrentPost.UserName != CurrentUser.UserName) {
            if(!CurrentPost.Likes.Contains(User.Identity.Name) && !CurrentUser.Liked.Contains((int)CurrentPost.PostId)) {
                CurrentPost.Likes.Add(User.Identity.Name);
                CurrentUser.Liked.Add(PostId);
            }
            else if(CurrentPost.Likes.Contains(User.Identity.Name) && CurrentUser.Liked.Contains((int)CurrentPost.PostId)) {
                CurrentPost.Likes.Remove(User.Identity.Name);
                CurrentUser.Liked.Remove(PostId);
            }
        }
        else {
            return StatusCode(StatusCodes.Status400BadRequest);
        }

        if(ModelState.IsValid) {
            _db.Update(CurrentPost);
            _db.SaveChanges();
            _db.Update(CurrentUser);
            _db.SaveChanges();
            return StatusCode(StatusCodes.Status200OK);
        }

        return StatusCode(StatusCodes.Status500InternalServerError);
    }
}