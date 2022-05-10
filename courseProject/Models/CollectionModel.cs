﻿using System.ComponentModel.DataAnnotations;

namespace courseProject.Models
{
    public class Collection
    {
        [Key] public int Id { get; set; }
        [Required] public string Name { get; set; }
        [Required] public string Description { get; set; }
        public string? StringFieldName1 { get; set; }
        public string? StringFieldName2 { get; set; }
        public string? StringFieldName3 { get; set; }
        public string? IntFieldName1 { get; set; }
        public string? IntFieldName2 { get; set; }
        public string? IntFieldName3 { get; set; }
        public string? DateTimeFieldName1 { get; set; }
        public string? DateTimeFieldName2 { get; set; }
        public string? DateTimeFieldName3 { get; set; }
        public string? BoolFieldName1 { get; set; }
        public string? BoolFieldName2 { get; set; }
        public string? BoolFieldName3 { get; set; }

        [Required] public DateTime CreationData { get; set; } = DateTime.Now;
        [Required] public User Creater { get; set; }
        public List<Item> Items { get; set; } = new List<Item>();
        [Required] public Topic Topic { get; set; }
    }
}
