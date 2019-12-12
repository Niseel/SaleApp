using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

using System.Text;

namespace ApplicationCore.DTOs
{
    public class OrderDto
    {
        [Key]
        [Required]
        [Display(Name = "Mã Hóa Đơn")]
        public int ID { get; set; }
        [Required]
        [Display(Name = "Số Tiền")]
        public decimal Amount { get; set; }
        [Required]
        [Display(Name = "Số Điện Thoại")]
        public string Phone { get; set; }
        [Required]
        [Display(Name = "Địa Chỉ")]
        public string Address { get; set; }
        [Required]
        [Display(Name = "Phương Thức Thanh Toán")]
        public string MethodPaying { get; set; }
        [Display(Name = "Ngân Hàng")]
        public string BankBrand { get; set; }
        [Display(Name = "Số Thẻ")]
        public string CardNumber { get; set; }
        [Display(Name = "Ghi Chú")]
        public string Note { get; set; }
        [Required]
        [Display(Name = "Trạng Thái Hóa Đơn")]
        public int Status { get; set; }

        [Required]
        [Display(Name = "Mã Khách Hàng")]
        public int UserID { get; set; }
        [Required]
        [Display(Name = "Mã Nhân Viên")]
        public int AdminID { get; set; }
    }
}
