using ApplicationCore.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SaleApp.ViewModels
{
    public class HomeIndexVm
    {
        public PaginatedList<ProductDto> Products { get; set; }
        public IEnumerable<CustomList> Brand { get; set; }
        public IEnumerable<CustomList> Category { get; set; }
        public IEnumerable<ProductDto> ViewProducts { set; get; }
        public IEnumerable<ProductDto> PayProducts { set; get; }
        public string SearchString { get; set; }
        public int PageSize { get; set; }
        public int Total { get; set; }
    }
}
