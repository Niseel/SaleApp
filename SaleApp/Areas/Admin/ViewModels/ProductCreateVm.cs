using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace SaleApp.ViewModels
{
    public class ProductCreateVm
    {

        [Required(ErrorMessage = "Please enter information")]
        [StringLength(45, MinimumLength = 3, ErrorMessage = "Name length must be more than 3")]
        [Column(TypeName = "nvachar")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Please enter information")]
        [Range(0, 9999, ErrorMessage = "Price has range between 0 ~ 9999")]
        [Column(TypeName = "decimal(6,2)")]
        public decimal Price { get; set; }
        [Range(0, 100, ErrorMessage = "Price has range between 0 ~ 100")]
        [Column(TypeName = "int")]
        [Required]
        public int Sale { get; set; }
        public IFormFile Image { get; set; }
        [StringLength(1000, ErrorMessage = "Content has maxium length 10000 character")]
        [Column(TypeName = "nvachar")]
        public string Content { get; set; }
        [Required]
        [Column(TypeName = "smallint")]
        [Range(0, 1, ErrorMessage = "Hot has value 0 or 1")]
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
        [Display(Name = "Brand")]
        public int BrandID { get; set; }
        [Required]
        [Column(TypeName = "int")]
        [Range(0, 999, ErrorMessage = "CategoryID has value 0 or 999")]
        [Display(Name = "Category")]
        public int CategoryID { get; set; }

        public SelectList CategoryList { get; set; }
        public SelectList BrandList { get; set; }
        public SelectList StatusList { get; set; }

    }
}
