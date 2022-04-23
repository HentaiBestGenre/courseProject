using System.ComponentModel.DataAnnotations;

namespace courseProject.Models
{
    public class Items
    {
        [Key] public int Id { get; set; }
        [Required] public string Login { get; set; }
        [Required] public string Password { get; set; }
        [Required] public bool IsBlock { get; set; } = false;
    }
}
