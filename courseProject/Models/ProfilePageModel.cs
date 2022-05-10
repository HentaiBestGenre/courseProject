using System.ComponentModel.DataAnnotations;

namespace courseProject.Models
{
    public class ProfilePage
    {
        [Key] public int Id { get; set; }
        [Required] public string Description { get; set; }
        [Required] public DateTime Online { get; set; }
        [Required] public User User { get; set; }
    }
}
