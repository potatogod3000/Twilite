using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace Twilite.Models;

public class PostInfoModel {

    [Key]
    public int? PostId { get; set; }

    [Required]
    public string UserName { get; set; }

    [Required]
    [MaxLength(450)]
    [MinLength(2, ErrorMessage = "You must enter atleast 2 characters to be able to Post this message")]
    public string PostContent { get; set; }
    
    public int? Likes { get; set; }
}