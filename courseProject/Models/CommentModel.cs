using System.ComponentModel.DataAnnotations;

namespace courseProject.Models
{
    public class Comment
    {
        [Key] public int Id { get; set; }
        [Required] public string Text { get; set; }
        [Required] public string CreaterId { get; set; }
        [Required] public Item Item { get; set; }
        [Required] public DateTime CreationData { get; set; } = DateTime.Now; 
    }
}
