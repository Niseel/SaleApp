using System;
using System.Collections.Generic;
using System.Text;
using ApplicationCore.Interfaces;
using Infrastructure.Persistence.Repositories;

namespace Infrastructure.Persistence
{
    public class UnitOfWork : IUnitOfWork
    {

        private readonly SaleContext _context;

        public UnitOfWork(SaleContext context)
        {
            Brands = new BrandRepository(context);
            Categories = new CategoryRepository(context);
            Products = new ProductRepository(context);
            Users = new UserRepository(context);
            _context = context;
        }

        public IBrandRepository Brands { get; private set; }
        public ICategoryRepository Categories { get; private set; }
        public IProductRepository Products { get; private set; }
        public IUserRepository Users { get; private set; }
        public int Complete()
        {
            return _context.SaveChanges();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
