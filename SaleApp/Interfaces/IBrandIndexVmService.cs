using SaleApp.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SaleApp.Interfaces
{
    public interface IBrandIndexVmService
    {
        IndexVm GetBrandListVm(string searchString, int pageIndex = 1);
    }
}
