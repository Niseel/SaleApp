using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace ApplicationCore.DTOs
{
    public class SaveProductDto
    {
        [Key]
        [Required]
        public int ID { get; set; }
        [Required]
        [StringLength(45, MinimumLength = 6, ErrorMessage = "Name length must be more than 6")]
        [Column(TypeName = "nvachar")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Please enter information")]
        [Range(0, 9999, ErrorMessage = "Price has range between 0 ~ 9999")]
        [Column(TypeName = "decimal(6,2)")] 
        public decimal Price { get; set; }
        [Range(0, 100, ErrorMessage = "Price has range between 0 ~ 100")]
        [Column(TypeName = "int")]
        public int Sale { get; set; }
        [Column(TypeName = "nvachar")]
        public string ImagePath { get; set; }
        [Column(TypeName = "nvachar")]
        [StringLength(1000, ErrorMessage = "Content has maxium length 10000 character")]
        public string Content { get; set; }
        [Required]
        [Column(TypeName = "smallint")]
        [Range(0,1, ErrorMessage = "Hot has value 0 or 1")]
        public int Hot { get; set; }
        [Required]
        [Column(TypeName = "int")]
        [Range(0, 9999, ErrorMessage = "Amount has range between 0 ~ 9999")]
        public int Amount { get; set; }
        [Required]
        [Column(TypeName = "smallint")]
        [Range(0, 1, ErrorMessage = "Status has value 0 or 1")]
        public int Status { get; set; }
        [Required]
        [Column(TypeName = "int")]
        [Range(0, 999, ErrorMessage = "BrandID has value 0 or 999")]
        public int BrandID { get; set; }
        [Required]
        [Column(TypeName = "int")]
        [Range(0, 999, ErrorMessage = "CategoryID has value 0 or 999")]
        public int CategoryID { get; set; }
    }
}
