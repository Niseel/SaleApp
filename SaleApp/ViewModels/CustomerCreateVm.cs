using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace SaleApp.ViewModels
{
    public class CustomerCreateVm
    {
        [Required]
        [StringLength(45, MinimumLength = 3, ErrorMessage = "FirstName must be greater than 3")]
        [Column(TypeName = "nvarchar")]
        public string FirstName { get; set; }
        [Required]
        [StringLength(45, MinimumLength = 3, ErrorMessage = "LastName must be greater than 3")]
        [Column(TypeName = "nvarchar")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Please input email")]
        [DataType(DataType.EmailAddress)]
        [Column(TypeName = "nvarchar")]
        public string Mail { get; set; }

        [StringLength(45, MinimumLength = 6, ErrorMessage = "Password must be greater than 6")]
        [Required(ErrorMessage = "Please input your password")]
        [Column(TypeName = "nvarchar")]
        public string Password { get; set; }
    }
}
