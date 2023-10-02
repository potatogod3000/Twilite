using Twilite.Models;
using Twilite.Data;
using Microsoft.AspNetCore.Mvc;

namespace Twilite.ViewComponents;

public class Replies : ViewComponent {

    private readonly ApplicationDbContext _db;
    public Replies(ApplicationDbContext db) {
        _db = db;
    }

    public IViewComponentResult Invoke(ReplyInfoModel Reply, int PostId) {
        ViewBag.Reply = Reply;

        if(PostId != null && PostId != 0) {
            ViewBag.PostId = PostId;
        }

        return View("ShowReply", ViewBag);
    }
}