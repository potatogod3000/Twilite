using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Twilite.Models; 

public class UserProfileModel {
    
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }

    public string UserAvatarLocation { get; set; }  

    public string UserName { get; set; }

    public List<string>? Followers { get; set; }

    public List<string>? Following { get; set; }

    // Stores all the Posts ("PostId") that the user has liked
    public List<int>? Liked { get; set; }

    public UserProfileModel() {
        Followers ??= new List<string>();
        Following ??= new List<string>();
        Liked ??= new List<int>();
        UserAvatarLocation = "~/Images/user-avatars/user.png";
    }
}