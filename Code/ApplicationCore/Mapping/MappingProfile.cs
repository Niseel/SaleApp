using ApplicationCore.DTOs;
using ApplicationCore.Entities;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;

namespace ApplicationCore.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Brand, BrandDto>();
            CreateMap<SaveBrandDto, Brand>();
            CreateMap<BrandDto, Brand>();
            CreateMap<BrandDto, SaveBrandDto>();
            CreateMap<SaveBrandDto, BrandDto>();

            CreateMap<Category, CategoryDto>();
            CreateMap<SaveCategoryDto, Category>();
            CreateMap<CategoryDto, Category>();
            CreateMap<CategoryDto, SaveCategoryDto>();
            CreateMap<SaveCategoryDto, CategoryDto>();

            CreateMap<Product, ProductDto>();
            CreateMap<SaveProductDto, Product>();
            CreateMap<ProductDto, Product>();
            CreateMap<ProductDto, SaveProductDto>();
            CreateMap<SaveProductDto, ProductDto>();

            CreateMap<User, UserDto>();
            CreateMap<SaveUserDto, User>();
            CreateMap<UserDto, User>();
            CreateMap<UserDto, SaveUserDto>();
            CreateMap<SaveUserDto, UserDto>();
        }
    }
}
