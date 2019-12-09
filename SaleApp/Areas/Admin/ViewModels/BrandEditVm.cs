using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SaleApp.ViewModels
{
    public class BrandEditVm : BrandCreateVm
    {
        public int Id { get; set; }
        public string ExistPhotoPath { get; set; }
    }
}
