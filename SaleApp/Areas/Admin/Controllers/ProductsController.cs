using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using ApplicationCore.DTOs;
using ApplicationCore.Interfaces;
using AutoMapper;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SaleApp.Interfaces;
using SaleApp.ViewModels;

namespace SaleApp.Controllers
{
    [Area("Admin")]
    public class ProductsController : Controller
    {
        private readonly IProductService _service;
        private readonly IMapper _mapper;
        [Obsolete]
        private readonly IHostingEnvironment _hostingEnvironment;

        private readonly IProductIndexVmService _productService;
        [Obsolete]
        public ProductsController(IProductIndexVmService productService, IProductService service, IMapper mapper, IHostingEnvironment hostingEnvironment)
        {
            _productService = productService;
            _service = service;
            _mapper = mapper;
            _hostingEnvironment = hostingEnvironment;
        }
        public IActionResult Index(string searchString, string brand, string category, int pageIndex = 1, int pageSize = 5, int? status = null)
        {
            if (HttpContext.Session.GetInt32("LoginLevel") != 2)
            {
                ViewBag.checkLogin = 0;
                return View("../Home/AddCart");
            }
            ViewData["Brand"] = brand;
            ViewData["Category"] = category;
            ViewData["SearchString"] = searchString;

            IndexVm ProductIndexVM = _productService.GetProductListVm(searchString, brand, category, pageIndex, pageSize, status);
            ViewBag.pageSize = pageSize;
            ViewBag.status = status;
            return View(ProductIndexVM);
        }

        [HttpGet]
        public IActionResult Create()
        {
            if (HttpContext.Session.GetInt32("LoginLevel") != 2)
            {
                ViewBag.checkLogin = 0;
                return View("../Home/AddCart");
            }
            var x = _productService.GetList();
            ViewBag.BrandList = x.BrandList;
            ViewBag.CategoryList = x.CategoryList;
            ViewBag.StatusList = x.StatusList;
            return View();
        }
        [HttpPost]
        [Obsolete]
        public IActionResult Create(ProductCreateVm model)
        {
            if (HttpContext.Session.GetInt32("LoginLevel") != 2)
            {
                ViewBag.checkLogin = 0;
                return View("../Home/AddCart");
            }
            if (!ModelState.IsValid)
            {
                var x = _productService.GetList();
                ViewBag.BrandList = x.BrandList;
                ViewBag.CategoryList = x.CategoryList;
                ViewBag.StatusList = x.StatusList;
                return View();
            }

            var products = _service.GetAll();
            foreach (ProductDto item in products)
            {
                if (item.Name.ToLower() == model.Name.ToLower())
                {
                    var x = _productService.GetList();
                    ViewBag.BrandList = x.BrandList;
                    ViewBag.CategoryList = x.CategoryList;
                    ViewBag.StatusList = x.StatusList;
                    ViewBag.ProductDuplicateErrorMessage = "Error";
                    return View();
                }
            }

            string uniqueFileName = ProcessUploadedFile(model);

            SaveProductDto saveProductDto = new SaveProductDto()
            {
                Name = model.Name,
                Price = model.Price,
                Sale = model.Sale,
                Amount = model.Amount,
                BrandID = model.BrandID,
                CategoryID = model.CategoryID,
                Hot = model.Hot,
                Status = model.Status,
                Content = model.Content,
                ImagePath = uniqueFileName

            };

            _service.Add(saveProductDto);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Detail(int? id)
        {
            if (HttpContext.Session.GetInt32("LoginLevel") != 2)
            {
                ViewBag.checkLogin = 0;
                return View("../Home/AddCart");
            }
            if (id == null)
            {
                return NotFound();
            }
            var product = _service.GetProduct(id.Value);


            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }

        [HttpGet]
        public IActionResult Edit(int? id)
        {
            if (HttpContext.Session.GetInt32("LoginLevel") != 2)
            {
                ViewBag.checkLogin = 0;
                return View("../Home/AddCart");
            }
            if (id == null)
            {
                return NotFound();
            }

            var product = _service.GetProduct(id.Value);

            if (product == null)
            {
                return NotFound();
            }

            ProductEditVm ProductEditVM = new ProductEditVm()
            {
                ID = product.ID, // hidden
                ExistImagePath = product.ImagePath, // hidden

                Name = product.Name,
                Amount = product.Amount,
                Price = product.Price,
                Sale = product.Sale,
                Hot = product.Hot,
                Content = product.Content,
                BrandID = product.BrandID,
                CategoryID = product.CategoryID,
                Status = product.Status,
                
            };
            var x = _productService.GetList();
            ViewBag.BrandList = x.BrandList;
            ViewBag.CategoryList = x.CategoryList;
            ViewBag.StatusList = x.StatusList;
            return View(ProductEditVM);
        }

        [HttpPost]
        [Obsolete]
        public IActionResult Edit(ProductEditVm model)
        {
            if (HttpContext.Session.GetInt32("LoginLevel") != 2)
            {
                ViewBag.checkLogin = 0;
                return View("../Home/AddCart");
            }
            if (ModelState.IsValid)
            {
                var products = _service.GetAll();
                foreach (ProductDto item in products)
                {
                    if (model.Name.ToLower().Equals(item.Name.ToLower()) && model.ID != item.ID)
                    {
                        var x = _productService.GetList();
                        ViewBag.BrandList = x.BrandList;
                        ViewBag.CategoryList = x.CategoryList;
                        ViewBag.StatusList = x.StatusList;
                        ViewBag.ProductEditUnchangedErrorMessage = "Error";
                        return View(model);
                    }
                }

                ProductDto productDto = _service.GetProduct(model.ID);
                SaveProductDto saveProductDto = _mapper.Map<ProductDto, SaveProductDto>(productDto);
                saveProductDto.Name = model.Name;
                saveProductDto.Price = model.Price;
                saveProductDto.Sale = model.Sale;
                saveProductDto.Amount = model.Amount;
                saveProductDto.Hot = model.Hot;
                saveProductDto.BrandID = model.BrandID;
                saveProductDto.CategoryID = model.CategoryID;
                saveProductDto.Content = model.Content;
                saveProductDto.Status = model.Status;

                if (model.Image != null)
                {
                    if (model.ExistImagePath != null)
                    {
                        string filePath = Path.Combine(_hostingEnvironment.WebRootPath, "images", model.ExistImagePath);
                        System.IO.File.Delete(filePath);
                    }
                    saveProductDto.ImagePath = ProcessUploadedFile(model);
                }
                _service.Update(saveProductDto);
                return View("Detail", _service.GetProduct(saveProductDto.ID));

            }
            return View();
        }

        [Obsolete]
        private string ProcessUploadedFile(ProductCreateVm model)
        {
            string uniqueFileName = null;
            if (model.Image != null)
            {
                string uploadFolder = Path.Combine(_hostingEnvironment.WebRootPath, "images/products");
                uniqueFileName = Guid.NewGuid().ToString() + "_" + model.Image.FileName;
                string filePath = Path.Combine(uploadFolder, uniqueFileName);
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    model.Image.CopyTo(fileStream);
                }

            }

            return uniqueFileName;
        }

    }
}