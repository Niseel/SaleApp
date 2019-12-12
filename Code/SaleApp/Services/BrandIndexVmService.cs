using ApplicationCore.DTOs;
using ApplicationCore.Interfaces;
using Microsoft.AspNetCore.Mvc.Rendering;
using SaleApp.Interfaces;
using SaleApp.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SaleApp.Services
{
    public class BrandIndexVmService : IBrandIndexVmService
    {
        private int pageSize = 3;
        private readonly IBrandService _service;
        public BrandIndexVmService(IBrandService brandService)
        {
            _service = brandService;
        }

        public IndexVm GetBrandListVm(string searchString, int pageIndex)
        {
            int count;
            var brands = _service.GetBrands(searchString, pageIndex, pageSize, out count);
            return new IndexVm
            {
                //Brands = PaginatedList<BrandDto>.Create(brands, pageIndex, 5)
                Brands = new PaginatedList<BrandDto>(brands, count, pageIndex, pageSize)
            };
        }

        public BrandCreateVm GetList()
        {
            return new BrandCreateVm
            {
                StatusList = new SelectList(new List<SelectListItem>{
                    new SelectListItem { Text = "Unavailable", Value = "0"},
                    new SelectListItem { Text = "Available", Value = "1"}
                }, "Value", "Text", 1)
            };
        }

        public BrandEditVm GetEditList()
        {
            throw new NotImplementedException();
        }
    }
}
