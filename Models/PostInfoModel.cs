using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace Twilite.Models;

public class PostInfoModel {
    private static readonly UserManager<IdentityUser> UserManager;

    [Key]
    public int PostId { get; set; }

    [Required]
    public string UserName { get; set; }

    [Required]
    [StringLength(450, ErrorMessage = "The {0} must not exceed {1} characters.")]
    public string PostContent { get; set; }
}