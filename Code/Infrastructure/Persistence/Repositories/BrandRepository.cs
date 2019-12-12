using ApplicationCore.DTOs;
using ApplicationCore.Entities;
using ApplicationCore.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace Infrastructure.Persistence.Repositories
{
    public class BrandRepository : Repository<Brand>, IBrandRepository
    {
        public BrandRepository(SaleContext context) : base(context)
        {
        }

        // phuong thuc rieng
        public IEnumerable<CustomList> GetBrands()
        {
            List<CustomList> result = new List<CustomList>();
            var query = from b in MContext.Brands
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

            return result.ToList();
        }

        public IEnumerable<string> GetListBrands()
        {
            List<string> result = new List<string>();
            var query = from b in MContext.Brands
                        select b;
            foreach (var item in query)
            {
                result.Add(item.Name);
            }
            return result.ToList();
        }

        public IEnumerable<BrandDto> GetAllBrands(string searchString)
        {
            List<BrandDto> result = new List<BrandDto>();
            var query = from b in MContext.Brands
                        select new
                        {
                            BrandID = b.ID,
                            BrandName = b.Name
                        };

            if (!String.IsNullOrEmpty(searchString))
            {
                query = query.Where(s => s.BrandName.Contains(searchString));
            }
            foreach(var item in query)
            {
                BrandDto brand = new BrandDto()
                {
                    ID = item.BrandID,
                    Name = item.BrandName
                };
                result.Add(brand);
            }
            return result.ToList();
        }

        protected SaleContext MContext
        {
            get { return Context as SaleContext; }
        }

    }
}
