using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace ApplicationCore.DTOs
{
    public class UserDto
    {
        [Key]
        [Required]
        [Display(Name = "Mã Người Dùng")]
        public int ID { get; set; }
        [Required]
        [Display(Name = "Tên")]
        public string FirstName { get; set; }
        [Required]
        [Display(Name = "Họ")]
        public string LastName { get; set; }
        [Required]
        [Display(Name = "Email")]
        public string Mail { get; set; }
        [Required]
        [Display(Name = "Mật Khẩu")]
        public string Password { get; set; }
        [Required]
        [Display(Name = "Ngày Sinh")]
        [DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime Birth { get; set; }
        [Required]
        [Display(Name = "Số Điện Thoại")]
        public string Phone { get; set; }
        [Required]
        [Display(Name = "Địa Chỉ")]
        public string Address { get; set; }
        public string AvtPath { get; set; }
        public string Note { get; set; }
        [Required]
        [Display(Name = "Trạng Thái")]
        public int Status { get; set; }
        [Required]
        [Display(Name = "Cấp Độ")]
        public int Level { get; set; }
        public DateTime Created_at { get; set; }
        public DateTime Updated_at { get; set; }
    }
}
