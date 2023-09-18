using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Twilite.Models;

public class PostInfoModel {

    public PostInfoModel() {
        Likes ??= new List<string>();
        //Replies ??= new List<ReplyInfo>();
    }

    //Post
    [Key]
    public int? PostId { get; set; }

    [Required]
    public string UserName { get; set; }

    [Required]
    [MinLength(2, ErrorMessage = "You must enter at least 2 characters to be able to Post this message")]
    public string PostContent { get; set; }
    
    //Likes
    public List<string>? Likes { get; set; }

    //Reply
    /* public List<ReplyInfo> Replies { get; set; }
    
    [Keyless]
    public class ReplyInfo {

        public string? ReplyUserName { get; set; }

        [MaxLength(250)]
        [MinLength(2, ErrorMessage = "You must enter atleast 2 characters to be able to Post this message")]   
        public string? ReplyContent { get; set; }

        public string? ReplyLikes { get; set; }
    } */
}