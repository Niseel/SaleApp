
using ApplicationCore.DTOs;
using ApplicationCore.Entities;
using ApplicationCore.Interfaces;
using ApplicationCore.Specifications;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;

namespace ApplicationCore.Services
{
    public class BrandService : IBrandService // kế thừa IBrandService
    {
        //DependencyInjection private readonly IUnitOfWork _unitOfWork; private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public BrandService(IUnitOfWork unitOfWork , IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        //public PaginatedList<Brand> GetBrands(int pageIndex, int pageSize)
        //{
        //    var brands = _unitOfWork.Brands.GetAll();
        //    return PaginatedList<Brand>.Create(brands, pageIndex, pageSize);
        //    //return new PaginatedList<Brand>(movies, pageIndex, pageSize);
        //}

        public IEnumerable<BrandDto> GetAll()
        {
            var brands = _unitOfWork.Brands.GetAll();
            return _mapper.Map<IEnumerable<Brand>, IEnumerable<BrandDto>>(brands);
        }
        public IEnumerable<BrandDto> GetAllsearch(string searchString)
        {
            return _unitOfWork.Brands.GetAllBrands(searchString);
        }

        public BrandDto GetBrand(int id)
        {
            var brand = _unitOfWork.Brands.GetBy(id);
            return _mapper.Map<Brand, BrandDto>(brand);
        }

        public void Add(SaveBrandDto saveBrandDto)
        {
            var brand = _mapper.Map<SaveBrandDto, Brand>(saveBrandDto);
            _unitOfWork.Brands.Add(brand);
            _unitOfWork.Complete();
        }

        public void Delete(int? id)
        {
            var brand = _unitOfWork.Brands.GetBy(id.Value);
            if (brand != null)
            {
                _unitOfWork.Brands.Remove(brand);
                _unitOfWork.Complete();
            }
        }

        public void Update(SaveBrandDto saveBrandDto)
        {
            var brand = _unitOfWork.Brands.GetBy(saveBrandDto.ID);
            if (brand == null) return;

            _mapper.Map<SaveBrandDto, Brand>(saveBrandDto, brand);

            _unitOfWork.Complete();
        }

        public IEnumerable<BrandDto> GetBrands(string searchString, int pageIndex, int pageSize, out int count)
        {
            BrandSpecification brandFilterPaginated = new BrandSpecification(searchString, pageIndex, pageSize);
            BrandSpecification brandFilter = new BrandSpecification(searchString);

            var brands = _unitOfWork.Brands.Find(brandFilterPaginated);
            count = _unitOfWork.Brands.Count(brandFilter);

            return _mapper.Map<IEnumerable<Brand>, IEnumerable<BrandDto>>(brands);
        }
    }
}
