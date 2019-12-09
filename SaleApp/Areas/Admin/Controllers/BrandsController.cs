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
    public class BrandsController : Controller
    {
        private readonly IBrandService _service;
        private readonly IMapper _mapper;
        [Obsolete]
        private readonly IHostingEnvironment _hostingEnvironment;
        //public BrandsController(IBrandService service)
        //{
        //    _service = service;
        //}

        //
        private readonly IBrandIndexVmService _brandService;

        [Obsolete]
        public BrandsController(IBrandIndexVmService brandService, IBrandService service, IMapper mapper, IHostingEnvironment hostingEnvironment)
        {
            _brandService = brandService;
            _service = service;
            _mapper = mapper;
            _hostingEnvironment = hostingEnvironment;
        }
        //
        //public BrandIndexVm BrandIndexVM { get; set; }
        public IActionResult Index(string searchString, int pageIndex = 1)
        {
            IndexVm BrandIndexVM = _brandService.GetBrandListVm(searchString, pageIndex);
            return View(BrandIndexVM);
        }
        //public IActionResult Index(string sortOrder, 
        //                            string currentFilter, 
        //                            string searchString,
        //                            int? pageNumber,
        //                            int? pageSize
        //                            )
        //{
        //    ViewData["CurrentSort"] = sortOrder;
        //    ViewData["NameSortParm"] = sortOrder == "name_desc" ? "Name" : "name_desc";
        //    //ViewData["NameSortParm"] = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
        //    ViewData["DateSortParm"] = sortOrder == "Date" ? "date_desc" : "Date";
        //    ViewData["CurrentPageSize"] = pageSize;

        //    if (searchString != null)
        //    {
        //        pageNumber = 1;
        //    }
        //    else
        //    {
        //        searchString = currentFilter;
        //    }
        //    ViewData["CurrentFilter"] = searchString;

        //    var brands = _service.GetAll();

        //    if (!String.IsNullOrEmpty(searchString))
        //    {
        //        brands = brands.Where(s => s.Name.Contains(searchString));

        //    }

        //    switch (sortOrder)
        //    {
        //        case "name_desc":
        //            brands = brands.OrderByDescending(s => s.Name);
        //            break;
        //        case "Date":
        //            brands = brands.OrderBy(s => s.Created_at);
        //            break;
        //        case "date_desc":
        //            brands = brands.OrderByDescending(s => s.Created_at);
        //            break;
        //        case "Name":
        //            brands = brands.OrderBy(s => s.Name);
        //            break;
        //        default:
        //            brands = brands.OrderBy(s => s.ID);
        //            break;
        //    };

        //    ViewData["pageSize"] = new int[3] { 1, 3, 5 };

        //    //return View(await students.AsNoTracking().ToListAsync());
        //    //return View(brands.ToList());
        //    //int PageSize = pageSize;
        //    ViewData["pageNum"] = pageNumber;

        //    //return PaginatedList<Brand>.Create(brands, pageIndex, pageSize);

        //    //return View(GetMovies(pageNumber ?? 1, pageSize ?? 3));
        //    return View(PaginatedList<Brand>.Create(brands, pageNumber ?? 1, pageSize ?? 2));
                
        //}

        //public IActionResult Detail(int? id)
        //{
        //    //var x = _service.GetAll();
        //    return View(_service.GetBrand(id.Value));
        //}

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [Obsolete]
        public IActionResult Create(BrandCreateVm model)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
            string uniqueFileName = ProcessUploadedFile(model);

            SaveBrandDto saveBrandDto = new SaveBrandDto()
            {
                Name = model.Name,
                Status = model.Status,
                PhotoPath = uniqueFileName

            };

            _service.Add(saveBrandDto);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Detail(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var brand = _service.GetBrand(id.Value);


            if (brand == null)
            {
                return NotFound();
            }

            return View(brand);
        }

        [HttpGet]
        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var brand = _service.GetBrand(id.Value);

            if (brand == null)
            {
                return NotFound();
            }

            BrandEditVm BrandEditVM = new BrandEditVm()
            {
                
                Id = brand.ID,
                Name = brand.Name,
                Status = brand.Status,
                ExistPhotoPath = brand.PhotoPath,
                
            };

            return View(BrandEditVM);
            
        }
        [HttpPost]
        [Obsolete]
        public IActionResult Edit(BrandEditVm model)
        {
            if (ModelState.IsValid)
            {
                BrandDto brandDto = _service.GetBrand(model.Id);
                SaveBrandDto saveBrandDto = _mapper.Map<BrandDto, SaveBrandDto>(brandDto);
                saveBrandDto.Name = model.Name;
                saveBrandDto.Status = model.Status;
                //saveBrandDto.PhotoPath = model.ExistPhotoPath;

                if (model.Photo != null)
                {
                    if (model.ExistPhotoPath != null)
                    {
                        string filePath = Path.Combine(_hostingEnvironment.WebRootPath, "images", model.ExistPhotoPath);
                        System.IO.File.Delete(filePath);
                    }
                    saveBrandDto.PhotoPath = ProcessUploadedFile(model);
                }
                _service.Update(saveBrandDto);
                return View("Detail", _service.GetBrand(saveBrandDto.ID));

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

            var brand = _service.GetBrand(id.Value);
            if (brand == null)
            {
                return NotFound();
            }

            return View(brand);
        }
        [HttpPost]
        public IActionResult Delete(int id)
        {
            _service.Delete(id);
            return RedirectToAction("Index");
        }
        private bool BrandExists(int id)
        {
            return _service.GetBrand(id) != null;
        }

        [Obsolete]
        private string ProcessUploadedFile(BrandCreateVm model)
        {
            string uniqueFileName = null;
            if (model.Photo != null)
            {
                string uploadFolder = Path.Combine(_hostingEnvironment.WebRootPath, "images/brands");
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