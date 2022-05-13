using System.ComponentModel.DataAnnotations;

namespace courseProject.Models
{
    public class CreateItemModel
    {
        [Required] public string Name { get; set; }
        [Required] public int CollectionId { get; set; }
        [Required] public string CreaterId { get; set; }
        public string? StringFieldValue1 { get; set; }
        public string? StringFieldValue2 { get; set; }
        public string? StringFieldValue3 { get; set; }
        public string? TextFieldValue1 { get; set; }
        public string? TextFieldValue2 { get; set; }
        public string? TextFieldValue3 { get; set; }
        public int? IntFieldValue1 { get; set; }
        public int? IntFieldValue2 { get; set; }
        public int? IntFieldValue3 { get; set; }
        public DateTime? DateTimeFieldValue1 { get; set; }
        public DateTime? DateTimeFieldValue2 { get; set; }
        public DateTime? DateTimeFieldValue3 { get; set; }
        public bool? BoolFieldValue1 { get; set; }
        public bool? BoolFieldValue2 { get; set; }
        public bool? BoolFieldValue3 { get; set; }

        [Required] public string[] tags { get; set; }
    }
}
