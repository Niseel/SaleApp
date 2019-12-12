using ApplicationCore.DTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace ApplicationCore.Interfaces
{
    public interface ICategoryService
    {
        // chứa các phương thức riêng của đối tượng Category và đc chạy thông qua CategoryDTO
        CategoryDto GetCate(int id);
        IEnumerable<CategoryDto> GetAll();
        //IEnumerable<Brand> GetBrands(int pageIndex, int pageSize);
        //IEnumerable<string> GetGenre();
        // add method
        void Add(SaveCategoryDto saveCategoryDto);
        void Delete(int? id);
        void Update(SaveCategoryDto saveCategoryDto);
    }
}
