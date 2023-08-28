using System.ComponentModel.DataAnnotations;

namespace Twilite.Models;

public class PostInfoModel {

    [Key]
    public int PostId { get; set; }

    [Required]
    public string UserName { get; set; }

    [Required]
    public string PostContent { get; set; }
}