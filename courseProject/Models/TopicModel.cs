using System.ComponentModel.DataAnnotations;

namespace courseProject.Models
{
    public class Topic
    {
        [Key] public int Id { get; set; }
        [Required] public string Name { get; set; }

        public List<Collection> collections { get; set; } = new List<Collection>();
    }
}
