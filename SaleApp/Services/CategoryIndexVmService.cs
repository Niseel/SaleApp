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
    public class CategoryIndexVmService : ICategoryIndexVmService
    {
        private readonly ICategoryService _service;
        public CategoryIndexVmService(ICategoryService categoryService)
        {
            _service = categoryService;
        }

        public IndexVm GetCategoryListVm(int pageIndex)
        {
            var categories = _service.GetAll();
            return new IndexVm
            {
                Categories = PaginatedList<CategoryDto>.Create(categories, pageIndex, 5)
            };
        }

        public CategoryCreateVm GetList()
        {
            return new CategoryCreateVm
            {
                StatusList = new SelectList(new List<SelectListItem>{
                    new SelectListItem { Text = "Unavailable", Value = "0"},
                    new SelectListItem { Text = "Available", Value = "1"}
                }, "Value", "Text", 1)
            };
        }
    }
}
