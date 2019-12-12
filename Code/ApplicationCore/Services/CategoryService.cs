using ApplicationCore.DTOs;
using ApplicationCore.Entities;
using ApplicationCore.Interfaces;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;

namespace ApplicationCore.Services
{
    public class CategoryService : ICategoryService // kế thừa ICategoryService
    {
        //DependencyInjection private readonly IUnitOfWork _unitOfWork; private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public CategoryService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public IEnumerable<CategoryDto> GetAll()
        {
            var cates = _unitOfWork.Categories.GetAll();
            return _mapper.Map<IEnumerable<Category>, IEnumerable<CategoryDto>>(cates);
        }

        public CategoryDto GetCate(int id)
        {
            var cate = _unitOfWork.Categories.GetBy(id);
            return _mapper.Map<Category, CategoryDto>(cate);
        }

        public void Add(SaveCategoryDto saveCategoryDto)
        {
            var cate = _mapper.Map<SaveCategoryDto, Category>(saveCategoryDto);
            _unitOfWork.Categories.Add(cate);
            _unitOfWork.Complete();
        }

        public void Delete(int? id)
        {
            var cate = _unitOfWork.Categories.GetBy(id.Value);
            if (cate != null)
            {
                _unitOfWork.Categories.Remove(cate);
                _unitOfWork.Complete();
            }
        }

        public void Update(SaveCategoryDto saveCategoryDto)
        {
            var cate = _unitOfWork.Categories.GetBy(saveCategoryDto.ID);
            if (cate == null) return;

            _mapper.Map<SaveCategoryDto, Category>(saveCategoryDto, cate);

            _unitOfWork.Complete();
        }

    }
}
