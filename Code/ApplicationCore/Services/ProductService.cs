using ApplicationCore.DTOs;
using ApplicationCore.Entities;
using ApplicationCore.Interfaces;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;


namespace ApplicationCore.Services
{
    public class ProductService : IProductService // kế thừa IProductService
    {
        //DependencyInjection private readonly IUnitOfWork _unitOfWork; private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public ProductService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public IEnumerable<ProductDto> GetAll()
        {
            var products = _unitOfWork.Products.GetAll();
            return _mapper.Map<IEnumerable<Product>, IEnumerable<ProductDto>>(products);
        }
        //public IEnumerable<ProductDto> GetAllProduct(string se)
        //{
        //    return _unitOfWork.Products.GetAllProduct();
        //    //return _mapper.Map<IEnumerable<Product>, IEnumerable<ProductDto>>(products);
        //}
        public IEnumerable<ProductDto> GetAllProduct(string searchString, string brand, string category, int? status)
        {
            return _unitOfWork.Products.GetAllProduct(searchString, brand, category, status);
            //return _mapper.Map<IEnumerable<Product>, IEnumerable<ProductDto>>(products);
        }
        public IEnumerable<ProductDto> GetAllProduct(string searchString, string brand, string category, string sort)
        {
            return _unitOfWork.Products.GetAllProduct(searchString, brand, category, sort);
        }

        public IEnumerable<CustomList> GetBrands()
        {
            return _unitOfWork.Brands.GetBrands();
        }

        public IEnumerable<CustomList> GetCategories()
        {
            return _unitOfWork.Categories.GetCategories();
        }

        public ProductDto GetProduct(int id)
        {
            return _unitOfWork.Products.Find(id);
            //return _mapper.Map<Product, ProductDto>(product);
        }

        public void Add(SaveProductDto saveProductDto)
        {
            var product = _mapper.Map<SaveProductDto, Product>(saveProductDto);
            _unitOfWork.Products.Add(product);
            _unitOfWork.Complete();
        }

        public void Delete(int? id)
        {
            var cate = _unitOfWork.Products.GetBy(id.Value);
            if (cate != null)
            {
                _unitOfWork.Products.Remove(cate);
                _unitOfWork.Complete();
            }
        }

        public void Update(SaveProductDto saveProductDto)
        {
            var cate = _unitOfWork.Products.GetBy(saveProductDto.ID);
            if (cate == null) return;

            _mapper.Map<SaveProductDto, Product>(saveProductDto, cate);

            _unitOfWork.Complete();
        }

        public IEnumerable<ProductDto> GetViewProducts(int amount)
        {
            return _unitOfWork.Products.GetViewProducts(amount);
        }

        public IEnumerable<ProductDto> GetPayProducts(int amount)
        {
            return _unitOfWork.Products.GetPayProducts(amount);
        }

        public IEnumerable<ProductDto> GetAllProductx()
        {
            return _unitOfWork.Products.GetAllProductx();
        }

        public IEnumerable<string> GetListBrands()
        {
            return _unitOfWork.Brands.GetListBrands();
        }

        public IEnumerable<string> GetListCategories()
        {
            return _unitOfWork.Categories.GetListCategories();
        }

    }
}