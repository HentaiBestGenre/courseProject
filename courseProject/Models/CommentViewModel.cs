using System.ComponentModel.DataAnnotations;

namespace courseProject.Models
{
    public class CreateComment
    {
        [Required] public string Text { get; set; }
        [Required] public string CreaterId { get; set; }
        [Required] public int ItemId { get; set; }
        [Required] public DateTime CreationData { get; set; } = DateTime.Now;
    }
}
