using ApplicationCore.DTOs;
using ApplicationCore.Entities;
using ApplicationCore.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Infrastructure.Persistence.Repositories
{
    public class ProductRepository : Repository<Product>, IProductRepository
    {
        public ProductRepository(SaleContext context) : base(context)
        {
        }

        // khong co phuong thuc rieng
        public IEnumerable<ProductDto> GetAllProduct(string searchString, string brand, string category, int? status)
        {
            //var products = MContext.Products.Include("Brand").Include("Category");
            var query = from p in MContext.Products
                        join c in MContext.Categories on p.CategoryID equals c.ID
                        join b in MContext.Brands on p.BrandID equals b.ID
                        //where p.BrandID == brand && p.CategoryID == category
                        select new
                        {
                            ProductID = p.ID,
                            ProductName = p.Name,
                            ProductPrice = p.Price,
                            ProductSale = p.Sale,
                            ProductStatus = p.Status,
                            CategoryName = c.Name,
                            BrandName = b.Name
                        };

            //query = query.Where(x => x.);
            if (status != null)
            {
                query = query.Where(s => s.ProductStatus == status);
            }

            if (!String.IsNullOrEmpty(searchString))
            {
                query = query.Where(s => s.ProductName.Contains(searchString));
            }
            if (!String.IsNullOrEmpty(brand))
            {
                query = query.Where(s => s.BrandName.Contains(brand));
            }
            if (!String.IsNullOrEmpty(category))
            {
                query = query.Where(s => s.CategoryName.Contains(category));
            }

            var x = query.ToList();
            List<ProductDto> listResult = new List<ProductDto>();
            foreach (var item in x)
            {
                ProductDto products = new ProductDto()
                {
                    ID = item.ProductID,
                    Name = item.ProductName,
                    Price = item.ProductPrice,
                    Sale = item.ProductSale,
                    Status = item.ProductStatus,
                    BrandName = item.BrandName,
                    CategoryName = item.CategoryName
                };
                listResult.Add(products);

            }

            return listResult;
        }
        
        public IEnumerable<ProductDto> GetAllProduct(string searchString, string brand, string category, string sort)
        {
            var query = from p in MContext.Products
                        join c in MContext.Categories on p.CategoryID equals c.ID
                        join b in MContext.Brands on p.BrandID equals b.ID
                        where p.Status == 1
                        select new
                        {
                            ProductID = p.ID,
                            ProductName = p.Name,
                            ProductPrice = p.Price,
                            ProductSale = p.Sale,
                            ProductView = p.View,
                            ProductPay = p.Pay,
                            ProductImage = p.ImagePath,
                            CategoryName = c.Name,
                            BrandName = b.Name
                        };

            if (!String.IsNullOrEmpty(searchString))
            {
                query = query.Where(s => s.ProductName.Contains(searchString));
            }
            if (!String.IsNullOrEmpty(brand))
            {
                query = query.Where(s => s.BrandName.Contains(brand));
            }
            if (!String.IsNullOrEmpty(category))
            {
                query = query.Where(s => s.CategoryName.Contains(category));
            }

            switch(sort)
            {
                case "nameAsc":
                    query = query.OrderBy(s => s.ProductName);
                    break;
                case "nameDesc":
                    query = query.OrderByDescending(s => s.ProductName);
                    break;
                case "priceAsc":
                    query = query.OrderBy(s => s.ProductPrice);
                    break;
                case "priceDesc":
                    query = query.OrderByDescending(s => s.ProductPrice);
                    break;
                default:
                    query = query.OrderBy(s => s.ProductName);
                    break;
            }


            var x = query.ToList();
            List<ProductDto> listResult = new List<ProductDto>();
            foreach (var item in x)
            {
                ProductDto products = new ProductDto()
                {
                    ID = item.ProductID,
                    Name = item.ProductName,
                    Price = item.ProductPrice,
                    Sale = item.ProductSale,
                    View = item.ProductView,
                    Pay = item.ProductPay,
                    ImagePath = item.ProductImage,
                    BrandName = item.BrandName,
                    CategoryName = item.CategoryName
                };
                listResult.Add(products);

            }

            return listResult;
        }

        public ProductDto Find(int? id)
        {
            var query = from p in MContext.Products
                        join c in MContext.Categories on p.CategoryID equals c.ID
                        join b in MContext.Brands on p.BrandID equals b.ID
                        where p.ID == id
                        select new
                        {
                            ProductID = p.ID,
                            ProductName = p.Name,
                            ProductPrice = p.Price,
                            ProductSale = p.Sale,
                            ProductAmount = p.Amount,
                            ProductPay = p.Pay,
                            ProductHot = p.Hot,
                            ProductView = p.View,
                            ProductStatus = p.Status,
                            ProductCreatedAt = p.Created_at,
                            ProductUpdatedAt = p.Updated_at,
                            ProductBrandID = p.BrandID,
                            ProductCategoryID = p.CategoryID,
                            ProductBrandName = b.Name,
                            ProductCategoryName = c.Name,
                            ProdcutImage = p.ImagePath
                        };
            var item = query.First();
            ProductDto products = new ProductDto()
            {
                ID = item.ProductID,
                Name = item.ProductName,
                Price = item.ProductPrice,
                Sale = item.ProductSale,
                Amount = item.ProductAmount,
                Pay = item.ProductPay,
                Hot = item.ProductHot,
                View = item.ProductView,
                Status = item.ProductStatus,
                StatusName = item.ProductStatus == 1 ? "Availability" : "Unavailable",
                Created_at = item.ProductCreatedAt,
                Updated_at = item.ProductUpdatedAt,
                BrandID = item.ProductBrandID,
                CategoryID = item.ProductCategoryID,
                BrandName = item.ProductBrandName,
                CategoryName = item.ProductCategoryName,
                ImagePath = item.ProdcutImage
                
            };
            return products;
        }

        public IEnumerable<ProductDto> GetViewProducts(int amout)
        {
            var query = (from p in MContext.Products
                         orderby p.View
                         where p.Status == 1
                         select new
                        {
                            ProductID = p.ID,
                            ProductName = p.Name,
                            ProductPrice = p.Price,
                            ProductView = p.View,
                            ProductSale = p.Sale,
                            ProdcutImage = p.ImagePath
                        }).Take(amout);

            var x = query.ToList();

            List<ProductDto> listResult = new List<ProductDto>();
            foreach (var item in x)
            {
                ProductDto products = new ProductDto()
                {
                    ID = item.ProductID,
                    Name = item.ProductName,
                    Price = item.ProductPrice,
                    View = item.ProductView,
                    Sale = item.ProductSale,
                    ImagePath = item.ProdcutImage
                };
                listResult.Add(products);

            }
            return listResult;
        }

        public IEnumerable<ProductDto> GetPayProducts(int amout)
        {
            var query = (from p in MContext.Products
                         orderby p.Pay
                         where p.Status == 1
                         select new
                         {
                             ProductID = p.ID,
                             ProductName = p.Name,
                             ProductPrice = p.Price,
                             ProductPay = p.Pay,
                             ProductSale = p.Sale,
                             ProdcutImage = p.ImagePath
                         }).Take(amout);

            var x = query.ToList();

            List<ProductDto> listResult = new List<ProductDto>();
            foreach (var item in x)
            {
                ProductDto products = new ProductDto()
                {
                    ID = item.ProductID,
                    Name = item.ProductName,
                    Price = item.ProductPrice,
                    Pay = item.ProductPay,
                    Sale = item.ProductSale,
                    ImagePath = item.ProdcutImage
                };
                listResult.Add(products);

            }
            return listResult;
        }

        public IEnumerable<ProductDto> GetAllProductx()
        {
            var query = from p in MContext.Products
                        join c in MContext.Categories on p.CategoryID equals c.ID
                        join b in MContext.Brands on p.BrandID equals b.ID
                        where p.Status == 1
                        select new
                        {
                            ProductID = p.ID,
                            ProductName = p.Name,
                            ProductPrice = p.Price,
                            ProductSale = p.Sale,
                            ProductStatus = p.Status,
                            ProductImage = p.ImagePath,
                            CategoryName = c.Name,
                            BrandName = b.Name
                        };

            var x = query.ToList();
            List<ProductDto> listResult = new List<ProductDto>();
            foreach (var item in x)
            {
                ProductDto products = new ProductDto()
                {
                    ID = item.ProductID,
                    Name = item.ProductName,
                    Price = item.ProductPrice,
                    Sale = item.ProductSale,
                    Status = item.ProductStatus,
                    ImagePath = item.ProductImage,
                    BrandName = item.BrandName,
                    CategoryName = item.CategoryName
                };
                listResult.Add(products);
            }

            return listResult;
        }


        protected SaleContext MContext
        {
            get { return Context as SaleContext; }
        }
    }
}
