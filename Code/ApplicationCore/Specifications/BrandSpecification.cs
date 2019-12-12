using ApplicationCore.Entities;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace ApplicationCore.Specifications
{
    public class BrandSpecification : Specification<Brand>
    {
        public BrandSpecification(string searchString) 
            : base(MakeCriteria(searchString))
        {
        }

        public BrandSpecification(string searchString, int pageIndex, int pageSize) 
            : this(searchString)
        {
            ApplyPaging(pageIndex, pageSize);
        }

        private static Expression<Func<Brand, bool>> MakeCriteria(string searchString)
        {
            Expression<Func<Brand, bool>> predicate = m => true;

            if (!string.IsNullOrEmpty(searchString))
            {
                predicate = m => m.Name.Contains(searchString);
            }

            return predicate;
        }
    }
}
