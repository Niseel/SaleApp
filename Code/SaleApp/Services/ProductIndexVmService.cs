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
    public class ProductIndexVmService : IProductIndexVmService
    {
        private readonly IProductService _service;
        public ProductIndexVmService(IProductService productService)
        {
            _service = productService;
        }

        public ProductCreateVm GetList()
        {
            var brands = _service.GetBrands();
            var categories = _service.GetCategories();
            return new ProductCreateVm
            {
                BrandList = new SelectList(brands, "ID", "Value"),
                CategoryList = new SelectList(categories, "ID", "Value"),
                StatusList = new SelectList(new List<SelectListItem>
                {
                    new SelectListItem { Text = "Unavailable", Value = "0"},
                    new SelectListItem { Text = "Availability", Value = "1"}
                }, "Value", "Text", 1)
            };
        }

        public HomeIndexVm GetProductIndex(int amount)
        {
            return new HomeIndexVm
            {
                ViewProducts = _service.GetViewProducts(amount),
                PayProducts = _service.GetPayProducts(amount)
            };
        }

        public IndexVm GetProductListVm(string searchString, string brand, string category, int pageIndex = 1, int pageSize = 5, int? status = null)
        {
            var products = _service.GetAllProduct(searchString, brand, category, status);
            var brands = _service.GetBrands();
            var categories = _service.GetCategories();
            return new IndexVm
            {
                Total = products.Count(),
                brand = new SelectList(brands, "Key", "Value"),
                category = new SelectList(categories, "Key", "Value"),
                Products = PaginatedList<ProductDto>.Create(products, pageIndex, pageSize)
            };
        }

        public HomeIndexVm GetProductListVm(string searchString, string brand, string category, string sort, int pageIndex = 1, int pageSize = 6)
        {
            var products = _service.GetAllProduct(searchString, brand, category, sort);
            var brands = _service.GetBrands();
            var categories = _service.GetCategories();
            return new HomeIndexVm
            {
                Brand = brands,
                Category = categories,
                PageSize = pageSize,
                Total = products.Count(),
                Products = PaginatedList<ProductDto>.Create(products, pageIndex, pageSize)
            };
        }

    }
}
