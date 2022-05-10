using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace courseProject.Models
{
    [Index(nameof(Id), nameof(Name), IsUnique = true)]
    public class Tag
    {
        [Key]
        public int Id { get; set; }
        [Required] public string Name { get; set; }

        public List<Item> Items { get; set; }
    }
}
