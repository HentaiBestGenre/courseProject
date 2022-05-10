using System.ComponentModel.DataAnnotations;

namespace courseProject.Models
{
    public class ProfileViewModel
    {
        [Required] public ProfilePage ProfilePage { get; set; }
        [Required] public User User { get; set; }
        public IEnumerable<Collection>? Collections { get; set; }
    }
}
