﻿using System.ComponentModel.DataAnnotations;

namespace courseProject.Models
{
    public class Item
    {
        [Key] public int Id { get; set; }
        [Required] public string Name { get; set; }
        public string? StringFieldValue1 { get; set; }
        public string? StringFieldValue2 { get; set; }
        public string? StringFieldValue3 { get; set; }
        public int? IntFieldValue1 { get; set; }
        public int? IntFieldValue2 { get; set; }
        public int? IntFieldValue3 { get; set; }
        public DateTime? DateTimeFieldValue1 { get; set; }
        public DateTime? DateTimeFieldValue2 { get; set; }
        public DateTime? DateTimeFieldValue3 { get; set; }
        public bool? BoolFieldValue1 { get; set; }
        public bool? BoolFieldValue2 { get; set; }
        public bool? BoolFieldValue3 { get; set; }

        [Required] public DateTime CreationData { get; set; } = DateTime.Now;
        [Required] public Collection Collection { get; set; }
        [Required] public string CreaterId { get; set; }
        [Required] public List<Tag> Tags { get; set; }
        public List<Comment> Comments { get; set; } = new List<Comment>();
    }
}
