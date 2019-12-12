using ApplicationCore.DTOs;
using ApplicationCore.Entities;
using ApplicationCore.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace Infrastructure.Persistence.Repositories
{
    public class CategoryRepository : Repository<Category>, ICategoryRepository
    {
        public CategoryRepository(SaleContext context) : base(context)
        {
        }

        // phuong thuc rieng
        public IEnumerable<CustomList> GetCategories()
        {
            List<CustomList> result = new List<CustomList>();
            var query = from b in MContext.Categories
                        select b;
            foreach (var item in query)
            {
                CustomList x = new CustomList()
                {
                    ID = item.ID,
                    Key = item.Name,
                    Value = item.Name
                };
                result.Add(x);
            }

            return result;
        }

        public IEnumerable<string> GetListCategories()
        {
            List<string> result = new List<string>();
            var query = from b in MContext.Categories
                        select b;
            foreach (var item in query)
            {
                result.Add(item.Name);
            }

            return result;
        }

        protected SaleContext MContext
        {
            get { return Context as SaleContext; }
        }

    }
}
