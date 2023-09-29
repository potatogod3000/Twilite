using System.ComponentModel.DataAnnotations;
using System.Diagnostics.Contracts;
using Microsoft.AspNetCore.Mvc;
using Twilite.Migrations;

namespace Twilite.Models;
public class ReplyInfoModel {

    public ReplyInfoModel() {
        Likes ??= new List<string>();
    }

    [Key]
    public int ReplyId { get; set; }

    [Required]
    public string UserName { get; set; }

    [Required]
    public string ReplyContent { get; set; }

    public List<string>? Likes { get; set; }

    public int PostId { get; set; }

    public PostInfoModel Post { get; set; }
}
