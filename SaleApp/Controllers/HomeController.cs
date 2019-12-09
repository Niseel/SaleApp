using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Threading.Tasks;
using ApplicationCore.DTOs;
using ApplicationCore.Interfaces;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using SaleApp.Interfaces;
using SaleApp.ViewModels;

namespace SaleApp.Controllers
{

    public class HomeController : Controller
    {
        private readonly IProductService _serviceProduct;
        private readonly IUserService _serviceCustomer;
        private readonly IProductIndexVmService _productService;
        public HomeController(IProductIndexVmService productService, IProductService serviceProduct, IUserService serviceCustomer)
        {
            _serviceProduct = serviceProduct;
            _productService = productService;
            _serviceCustomer = serviceCustomer;
        }
        public IActionResult Index()
        {
            HomeIndexVm ProductIndexVM = _productService.GetProductIndex(8);
            return View(ProductIndexVM);
        }
        public IActionResult SingleProduct(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var product = _serviceProduct.GetProduct(id.Value);


            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }
        public IActionResult Products(string searchString, string brand, string category, string sort, int pageIndex = 1, int pageSize = 6)
        {
            HomeIndexVm ProductsVM = _productService.GetProductListVm(searchString, brand, category, sort, pageIndex, pageSize);
            ViewBag.pageSize = pageSize;
            return View(ProductsVM);
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Registration()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Registration(CustomerCreateVm model)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
            string passHash;
            using (MD5 md5Hash = MD5.Create())
            {
                passHash = MD5Hash.GetMd5Hash(md5Hash, model.Password);
            }

            SaveUserDto saveUserDto = new SaveUserDto()
            {
                FirstName = model.FirstName,
                LastName = model.LastName,
                Mail = model.Mail,
                Password = passHash
            };

            _serviceCustomer.Add(saveUserDto);
            ViewBag.check = 1;
            return View();
        }
    }

}