using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SaleApp.ViewModels
{
    public class CustomerLoginVm
    {
        [Required(ErrorMessage = "Please input Mail")]
        public string CustomerMail { get; set; }
        [Required(ErrorMessage = "Please input Password")]
        [StringLength(45, MinimumLength = 6, ErrorMessage = "Password length must be more than 6")]
        public string CustomerPassword { get; set; }
    }
}
