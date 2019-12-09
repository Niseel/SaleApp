using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

using System.Text;

namespace ApplicationCore.DTOs
{
    public class DetailOrderDto
    {
        [Key]
        [Required]
        [Display(Name = "Mã Chi Tiết Hóa Đơn")]
        public int ID { get; set; }
        [Required]
        [Display(Name = "Số Lượng Sản Phẩm")]
        public int Qty { get; set; }
        [Required]
        [Display(Name = "Đơn Giá")]
        public decimal Price { get; set; }
        [Required]
        [Display(Name = "Thành Tiền")]
        public decimal Total { get; set; }

        [Required]
        [Display(Name = "Mã Sản Phẩm")]
        public int ProductID { get; set; }
        [Required]
        [Display(Name = "Mã Hóa Đơn")]
        public int OrderID { get; set; }

        [Display(Name = "Sản Phẩm")]
        public int ProductName { get; set; }
        [Display(Name = "Thương Hiệu")]
        public string BrandName { get; set; }
        [Display(Name = "Thể Loại")]
        public string CategoryName { get; set; }

    }
}
