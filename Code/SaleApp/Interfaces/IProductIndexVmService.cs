using ApplicationCore.DTOs;
using SaleApp.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SaleApp.Interfaces
{
    public interface IProductIndexVmService
    {
        IndexVm GetProductListVm(string searchString, string brand, string category, 
            int pageIndex = 1, int pageSize = 5, int? status = null);

        ProductCreateVm GetList();

        HomeIndexVm GetProductIndex(int amount);
        HomeIndexVm GetProductListVm(string searchString, string brand, string category,
            string sort, int pageIndex = 1, int pageSize = 6);

    }
}
