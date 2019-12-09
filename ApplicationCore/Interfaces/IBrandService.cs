using System.Collections.Generic;
using ApplicationCore.DTOs;
using ApplicationCore.Entities;

namespace ApplicationCore.Interfaces
{
    public interface IBrandService
    {
        // chứa các phương thức riêng của đối tượng Brand và đc chạy thông qua BrandDTO
        BrandDto GetBrand(int id);
        IEnumerable<BrandDto> GetAll();
        IEnumerable<BrandDto> GetAllsearch(string searchString);
        IEnumerable<BrandDto> GetBrands(string searchString, int pageIndex, int pageSize, out int count);
        //IEnumerable<string> GetGenre();
        // add method
        void Add(SaveBrandDto saveBrandDto);
        void Delete(int? id);
        void Update(SaveBrandDto saveBrandDto);
    }
}
