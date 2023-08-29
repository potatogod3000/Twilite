using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Twilite.Models;

public class UserInfoModel {
    
    [Key]
    public int UserId { get; set; }

    [Required]
    [DisplayName("E-Mail ID")]
    public string Email { get; set; }

    [Required]
    [DisplayName("User Name")]
    [MaxLength(24)]
    
    public string UserName { get; set; }

    [Required]
    [MaxLength(24)]
    [MinLength(8, ErrorMessage = "The Password must be atleast 8 characters long (Maximum of 24 characters)")]
    public string Password { get; set; }
    
    public string Country { get; set; }
}