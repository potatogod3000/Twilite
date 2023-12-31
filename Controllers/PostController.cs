using Twilite.Models;
using Twilite.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.EntityFrameworkCore;
using System.Text.Json;
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
        Post.PostedDate = $"{DateTime.UtcNow.ToString("HH:mm")} {DateTime.UtcNow.ToString("MMM dd, yyyy")}";

        if(Post.PostContent == null) {
            ModelState.AddModelError("", "You haven't entered anything in the input area.");
        }
        else {
            _db.Posts.Update(Post);
            _db.SaveChanges();

            TempData["PostMessage"] = "Your post is now online!";
            return RedirectToAction("Explore", "Actions");        
        }
        
        return View(Post);
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
        
        Post.Likes = PostInfoObj.Likes;
        Post.PostEditedDate = $"{DateTime.UtcNow.ToString("HH:mm")} {DateTime.UtcNow.ToString("MMM dd, yyyy")}";

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
    [HttpPost]
    public IActionResult AddFollower(string PostUserName, string CurrentUserName) {
        try {
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
                    return StatusCode(StatusCodes.Status200OK);
                }
            }
        }
        catch(Exception ex) {
            BadRequest( new {message = ex.Message, status = 400} );
        }
        
        return StatusCode(StatusCodes.Status500InternalServerError);
    }

    [Authorize]
    [HttpPost]
    public IActionResult RemoveFollower(string PostUserName, string CurrentUserName) {
        try {
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
                return StatusCode(StatusCodes.Status200OK);
            }
        }
        catch(Exception ex) {
            BadRequest( new {message = ex.Message, status = 400} );
        }
        
        return StatusCode(StatusCodes.Status500InternalServerError);
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
            return StatusCode(StatusCodes.Status406NotAcceptable);
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

    [Authorize]
    [HttpGet]
    public IActionResult Replies(int PostId ,int ReplyId) {
        PostInfoModel Post = _db.Posts.Include(x => x.Replies).FirstOrDefault(x => x.PostId == PostId);
        ViewBag.Post = Post;

        if(ReplyId != null) {
            ViewBag.Reply = Post.Replies.FirstOrDefault(r => r.ReplyId == ReplyId);
        }

        return View(ViewBag);
    }

    [Authorize]
    [HttpPost]
    public IActionResult Replies(string ReplyString, int PostId) {
        PostInfoModel Post = _db.Posts.Include(r => r.Replies).FirstOrDefault(p => p.PostId == PostId);

        ReplyInfoModel Reply = new() {
            UserName = User.Identity.Name,
            ReplyContent = ReplyString,
            Post = Post
        };


        if(Reply.ReplyContent == null || Reply.ReplyContent == "") {
            return StatusCode(StatusCodes.Status400BadRequest);
        }
        else if(Post != null && Reply.ReplyContent != null && Reply.ReplyContent != "") {
            _db.Replies.Add(Reply);
            _db.SaveChanges();

            return StatusCode(StatusCodes.Status200OK);
        }
        else {
           return StatusCode(StatusCodes.Status500InternalServerError);
        }
    }

    [Authorize]
    public IActionResult ReplyLikes(int PostId, int ReplyId) {
        ReplyInfoModel Reply = _db.Posts.Include(r => r.Replies)
                                        .FirstOrDefault(x => x.PostId == PostId).Replies
                                        .FirstOrDefault(x => x.ReplyId == ReplyId);

        if(Reply == null) {
            return StatusCode(StatusCodes.Status400BadRequest);
        }
        else if(Reply.UserName != User.Identity.Name) {
            if(Reply.Likes.Contains(User.Identity.Name)) {
                Reply.Likes.Remove(User.Identity.Name);
            }
            else {
                Reply.Likes.Add(User.Identity.Name);
            }

            _db.Replies.Update(Reply);
            _db.SaveChanges();
            return StatusCode(StatusCodes.Status200OK); 
        }
        else if(Reply.UserName == User.Identity.Name) {
            return StatusCode(StatusCodes.Status406NotAcceptable);
        }
        else {
            return StatusCode(StatusCodes.Status500InternalServerError);
        }
    }
}