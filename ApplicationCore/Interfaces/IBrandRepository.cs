using ApplicationCore.DTOs;
using ApplicationCore.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace ApplicationCore.Interfaces
{
    public interface IBrandRepository : IRepository<Brand>
    {
        IEnumerable<CustomList> GetBrands();
        IEnumerable<string> GetListBrands();
        //không có phương thức riêng
        IEnumerable<BrandDto> GetAllBrands(string searchString);
    }
}
