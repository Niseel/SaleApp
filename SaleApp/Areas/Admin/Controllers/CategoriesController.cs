using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using ApplicationCore.DTOs;
using ApplicationCore.Entities;
using ApplicationCore.Interfaces;
using AutoMapper;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SaleApp.Interfaces;
using SaleApp.ViewModels;

namespace SaleApp.Controllers
{
    [Area("Admin")]
    public class CategoriesController : Controller
    {
        private readonly ICategoryService _service;
        private readonly IMapper _mapper;
        [Obsolete]
        private readonly IHostingEnvironment _hostingEnvironment;

        private readonly ICategoryIndexVmService _categoryService;

        [Obsolete]
        public CategoriesController(ICategoryIndexVmService categoryService, ICategoryService service, IMapper mapper, IHostingEnvironment hostingEnvironment)
        {
            _categoryService = categoryService;
            _service = service;
            _mapper = mapper;
            _hostingEnvironment = hostingEnvironment;
        }
        public IActionResult Index(int pageIndex = 1)
        {
            IndexVm CategoryIndexVM = _categoryService.GetCategoryListVm(pageIndex);
            return View(CategoryIndexVM); 
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [Obsolete]
        public IActionResult Create(CategoryCreateVm model)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
            string uniqueFileName = ProcessUploadedFile(model);

            SaveCategoryDto saveCategoryDto = new SaveCategoryDto()
            {
                Name = model.Name,
                Description = model.Description,
                Status = model.Status,
                PhotoPath = uniqueFileName

            };

            _service.Add(saveCategoryDto);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Detail(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var cate = _service.GetCate(id.Value);


            if (cate == null)
            {
                return NotFound();
            }

            return View(cate);
        }

        [HttpGet]
        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cate = _service.GetCate(id.Value);

            if (cate == null)
            {
                return NotFound();
            }

            CategoryEditVm CategoryEditVM = new CategoryEditVm()
            {
                Id = cate.ID,
                Name = cate.Name,
                Description = cate.Description,
                Status = cate.Status,
                ExistPhotoPath = cate.PhotoPath
            };

            return View(CategoryEditVM);

        }
        [HttpPost]
        [Obsolete]
        public IActionResult Edit(CategoryEditVm model)
        {
            if (ModelState.IsValid)
            {
                CategoryDto categoryDto = _service.GetCate(model.Id);
                SaveCategoryDto saveCategoryDto = _mapper.Map<CategoryDto, SaveCategoryDto>(categoryDto);
                saveCategoryDto.Name = model.Name;
                saveCategoryDto.Description = model.Description;
                saveCategoryDto.Status = model.Status;
                saveCategoryDto.PhotoPath = model.ExistPhotoPath;

                if (model.Photo != null)
                {
                    if (model.ExistPhotoPath != null)
                    {
                        string filePath = Path.Combine(_hostingEnvironment.WebRootPath, "images", model.ExistPhotoPath);
                        System.IO.File.Delete(filePath);
                    }
                    saveCategoryDto.PhotoPath = ProcessUploadedFile(model);
                }
                _service.Update(saveCategoryDto);
                return View("Detail", _service.GetCate(saveCategoryDto.ID));

            }
            return View();
        }

        [HttpGet]
        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cate = _service.GetCate(id.Value);
            if (cate == null)
            {
                return NotFound();
            }

            return View(cate);
        }
        [HttpPost]
        public IActionResult Delete(int id)
        {
            _service.Delete(id);
            return RedirectToAction("Index");
        }
        private bool BrandExists(int id)
        {
            return _service.GetCate(id) != null;
        }

        [Obsolete]
        private string ProcessUploadedFile(CategoryCreateVm model)
        {
            string uniqueFileName = null;
            if (model.Photo != null)
            {
                string uploadFolder = Path.Combine(_hostingEnvironment.WebRootPath, "images/categories");
                uniqueFileName = Guid.NewGuid().ToString() + "_" + model.Photo.FileName;
                string filePath = Path.Combine(uploadFolder, uniqueFileName);
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    model.Photo.CopyTo(fileStream);
                }

            }

            return uniqueFileName;
        }
    }


}