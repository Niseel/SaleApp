using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace ApplicationCore.DTOs
{
    public class ProductDto
    {
        [Key]
        [Required]
        [Display(Name = "Mã Sản Phẩm")]
        public int ID { get; set; }
        [Required]
        [Display(Name = "Tên Sản Phẩm")]
        [StringLength(45)]
        public string Name { get; set; }
        [Display(Name = "Đơn Giá")]
        public decimal Price { get; set; }
        [Display(Name = "Giảm giá")]
        public int Sale { get; set; }
        public string ImagePath { get; set; }
        [Display(Name = "Mô Tả")]
        public string Content { get; set; }
        [Display(Name = "Lượt Xem")]
        public int View { get; set; }
        [Display(Name = "Số Lượng Bán")]
        public int Pay { get; set; }
        [Display(Name = "Trạng Thái Hot")]
        public int Hot { get; set; }
        [Display(Name = "Số Lượng Trong Kho")]
        public int Amount { get; set; }
        [Required]
        [Display(Name = "Trạng Thái Hiển Thị")]
        public int Status { get; set; }

        [Display(Name = "Trạng Thái Hiển Thị")]
        public String StatusName { get; set; }
        public DateTime Created_at { get; set; }
        public DateTime Updated_at { get; set; }
        [Display(Name = "Mã Thương Hiệu")]
        public int BrandID { get; set; }
        [Display(Name = "Mã Thể Loại")]
        public int CategoryID { get; set; }


        [Display(Name = "Thương Hiệu")]
        public string BrandName { get; set; }
        [Display(Name = "Thể Loại")]
        public string CategoryName { get; set; }

    }
}
