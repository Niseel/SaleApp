using ApplicationCore.DTOs;
using ApplicationCore.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace ApplicationCore.Interfaces
{
    public interface ICategoryRepository : IRepository<Category>
    {
        IEnumerable<CustomList> GetCategories();
        IEnumerable<string> GetListCategories();
    }
}
