using ApplicationCore.DTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace ApplicationCore.Interfaces
{
    public interface IProductService
    {
        // chứa các phương thức riêng của đối tượng Product và đc chạy thông qua ProductDTO
        ProductDto GetProduct(int id);
        IEnumerable<ProductDto> GetAll();
        IEnumerable<ProductDto> GetAllProduct(string searchString, string brand, string category, int? status);
        IEnumerable<ProductDto> GetAllProduct(string searchString, string brand, string category, string sort);
        IEnumerable<string> GetListBrands();
        IEnumerable<string> GetListCategories();
        public IEnumerable<CustomList> GetBrands();
        public IEnumerable<CustomList> GetCategories();

        void Add(SaveProductDto saveProductDto);
        void Delete(int? id);
        void Update(SaveProductDto saveProductDto);

        IEnumerable<ProductDto> GetViewProducts(int amount);

        IEnumerable<ProductDto> GetPayProducts(int amount);
    }
}
