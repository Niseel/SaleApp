using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace ApplicationCore.DTOs
{
    public class BrandDto
    {
        [Key]
        [Required]
        [Display(Name = "Mã Thương Hiệu")]
        public int ID { get; set; }
        [Required]
        [Display(Name = "Tên Thương Hiệu")]
        [StringLength(45)]
        public string Name { get; set; }
        [Required]
        [Display(Name = "Trạng Thái")]
        public int Status { get; set; }
        public DateTime Created_at { get; set; }
        public DateTime Updated_at { get; set; }

        public string PhotoPath { get; set; }
    }
}
