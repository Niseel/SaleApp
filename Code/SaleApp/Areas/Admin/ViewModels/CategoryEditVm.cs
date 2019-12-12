using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SaleApp.ViewModels
{
    public class CategoryEditVm : CategoryCreateVm
    {
        public int Id { get; set; }
        public string ExistPhotoPath { get; set; }
    }
}
