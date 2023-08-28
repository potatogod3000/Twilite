using System.ComponentModel.DataAnnotations;

namespace Twilite.Models;

public class UserInfoModel {
    
    [Key]
    public int UserId { get; set; }

    [Required]
    public string Email { get; set; }

    [Required]
    public string UserName { get; set; }

    [Required]
    public string Password { get; set; }
    
    public string Country { get; set; }
}