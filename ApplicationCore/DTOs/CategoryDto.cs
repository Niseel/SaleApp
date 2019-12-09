using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace ApplicationCore.DTOs
{
    public class CategoryDto
    {
        [Key]
        [Required]
        [Display(Name = "Mã Thể Loại")]
        public int ID { get; set; }
        [Required]
        [Display(Name = "Tên Thể Loại")]
        [StringLength(45)]
        public string Name { get; set; }
        [Display(Name = "Mô tả")]
        public string Description { get; set; }
        [Required]
        [Display(Name = "Trạng Thái")]
        public int Status { get; set; }
        public DateTime Created_at { get; set; }
        public DateTime Updated_at { get; set; }
        public string PhotoPath { get; set; }
    }
}
