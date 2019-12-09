using ApplicationCore.DTOs;
using ApplicationCore.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace ApplicationCore.Interfaces
{
    public interface IProductRepository : IRepository<Product>
    {
        public IEnumerable<ProductDto> GetAllProductx();
        public IEnumerable<ProductDto> GetAllProduct(string searchString, string brand, string category, int? status);
        public IEnumerable<ProductDto> GetAllProduct(string searchString, string brand, string category, string sort);
        public ProductDto Find(int? id);
        public IEnumerable<ProductDto> GetViewProducts(int amout);
        public IEnumerable<ProductDto> GetPayProducts(int amout);

    }
}
