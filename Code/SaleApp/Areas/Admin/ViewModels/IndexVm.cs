using ApplicationCore.DTOs;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SaleApp.ViewModels
{
    public class IndexVm
    {
        public PaginatedList<BrandDto> Brands { get; set; }
        public PaginatedList<CategoryDto> Categories { get; set; }
        public PaginatedList<ProductDto> Products { get; set; }
        public PaginatedList<UserDto> Users { get; set; }

        public string SearchString { get; set; }
        public SelectList brand { get; set; }
        public SelectList category { get; set; }
        public int PageSize { get; set; }
        public int? Status { get; set; }
        public int Total { get; set; }

    }
}
