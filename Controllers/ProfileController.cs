using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Twilite.Data;
using Twilite.Models;

namespace Twilite.Controllers;

public class ProfileController : Controller {
    
    private readonly ApplicationDbContext _db;

    public ProfileController(ApplicationDbContext db) {
        _db = db;
    }

    [Authorize]
    [HttpGet]
    public async Task<IActionResult> UserProfile(string UserName) {
        List<PostInfoModel> Posts = _db.Posts.Include(r => r.Replies).ToList();
        List<ReplyInfoModel> Replies;
        List<List<ReplyInfoModel>> RepliesList = new();
        string comparison;
        
        if(UserName == null) {
            ViewData["CurrentUserProfile"] = _db.UserProfiles.FirstOrDefault(x => x.UserName == User.Identity.Name);
            ViewData["CurrentUserPosts"] = _db.Posts.Where(x => x.UserName == User.Identity.Name).Include(r => r.Replies).ToList();
            comparison = User.Identity.Name;
        }
        else {
            ViewData["CurrentUserProfile"] = _db.UserProfiles.FirstOrDefault(x => x.UserName == UserName);
            ViewData["CurrentUserPosts"] = _db.Posts.Where(x => x.UserName == UserName).Include(r => r.Replies).ToList();
            comparison = UserName;
        }

        foreach(var Post in Posts) {
            Replies = new();
            foreach(var Reply in Post.Replies) {
                if(Reply.UserName == comparison) {
                    Replies.Add(Reply);
                }
            }
            if(Replies.Count != 0) {
                RepliesList.Add(Replies);
            }
        }

        ViewData["AllReplies"] = RepliesList;

        return View("UserProfile", ViewData);
    }
}