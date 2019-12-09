using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace SaleApp.ViewModels
{
    public class CategoryCreateVm 
    {
        [Required]
        [StringLength(60, MinimumLength = 3, ErrorMessage = "Name length must be more than 3")]
        public string Name { get; set; }
        [Column(TypeName = "nvarchar")]
        public string Description { get; set; }
        [Required]
        [Range(0, 1, ErrorMessage = "Staus must 0 or 1")]
        [Column(TypeName = "smallint")]
        public int Status { get; set; }
        public IFormFile Photo { get; set; }
    }
}
