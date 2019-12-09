using SaleApp.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SaleApp.Interfaces
{
    public interface ICategoryIndexVmService
    {
        IndexVm GetCategoryListVm(int pageIndex = 1);
    }
}
