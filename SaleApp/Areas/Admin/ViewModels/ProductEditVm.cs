using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SaleApp.ViewModels
{
    public class ProductEditVm : ProductCreateVm
    {
        public int ID { get; set; }
        public string ExistImagePath { get; set; }
    }
}
