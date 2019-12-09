using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SaleApp.ViewModels
{
    public class UserEditVm : UserCreateVm
    {
        public int ID { get; set; }
        public string ExistAvtPath { get; set; }
    }
}
