using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Drawing;

namespace Twilite.Models; 

public class UserProfileModel {
    
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }

    public string UserName { get; set; }

    public List<string>? Followers { get; set; }

    public List<string>? Following { get; set; }

    // Stores all the Posts ("PostId") that the user has liked
    public List<int>? Liked { get; set; }

    public byte[] ProfilePictureBytes { get; set; }

    public UserProfileModel() {
        Followers ??= new List<string>();
        Following ??= new List<string>();
        Liked ??= new List<int>();

        ProfilePictureBytes ??= GetProfilePictureBytes();
    }

    public byte[] GetProfilePictureBytes() {        
        string defaultPicLocation = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Images/user-avatars/user.png");
        byte[] bytes = File.ReadAllBytes(defaultPicLocation);

        return bytes;
    }
}