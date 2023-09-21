using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Twilite.Models;

public class PostInfoModel {

    public PostInfoModel() {
        Likes ??= new List<string>();
        Replies ??= new List<ReplyInfo>();
    }

    //Post
    [Key]
    public int? PostId { get; set; }

    [Required]
    public string PostedDate { get; set; }

    public string? PostEditedDate { get; set; }

    [Required]
    public string UserName { get; set; }

    [Required]
    [MinLength(2, ErrorMessage = "You must enter at least 2 characters to be able to Post this message")]
    public string PostContent { get; set; }
    
    //Likes
    public List<string>? Likes { get; set; }

    //Reply
    public List<ReplyInfo> Replies { get; set; }
    
    public class ReplyInfo {

        [Key]
        public int ReplyId { get; set; }

        public string? ReplyUserName { get; set; }

        [MinLength(2, ErrorMessage = "You must enter atleast 2 characters to be able to Post this message")]   
        public string? ReplyContent { get; set; }

        public string? ReplyLikes { get; set; }
    }
}