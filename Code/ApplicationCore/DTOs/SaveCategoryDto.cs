using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace ApplicationCore.DTOs
{
    public class SaveCategoryDto
    {
        [Key]
        [Required]
        public int ID { get; set; }
        [Required]
        [StringLength(45, MinimumLength = 3, ErrorMessage = "Name length must be more than 3")]
        public string Name { get; set; }
        [Column(TypeName = "nvachar")]
        public string Description { get; set; }
        [Required]
        [Column(TypeName = "smallint")]
        [Range(0, 1, ErrorMessage = "Status has value 0 or 1")]
        public int Status { get; set; }
        public string PhotoPath { get; set; }
    }
}
