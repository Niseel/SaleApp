﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Threading.Tasks;
using ApplicationCore.DTOs;
using ApplicationCore.Interfaces;
using ApplicationCore.Session;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
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

        [HttpPost]
        public IActionResult Login(CustomerLoginVm model)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
            var customer = _serviceCustomer.GetUser(model.CustomerMail, model.CustomerPassword);
            if (customer == null)
            {
                return NotFound();
                //Account doesnt exist
                //Change to Product Envairoment when deploy
            }
            if (customer.Status == 0)
            {
                ViewBag.log = 0; 
                return View();
                //locked
            }
            else
            {
                //LoginSession loginUser = new LoginSession()
                //{
                //    LoginID = customer.ID,
                //    LoginName = customer.LastName,
                //    LoginMail = customer.Mail,
                //    LoginLevel = customer.Level,
                //    LoginStatus = customer.Status
                //};
                //HttpContext.Session.SetString("SessionUser", JsonConvert.SerializeObject(loginUser));
                //var userSession = JsonConvert.DeserializeObject<LoginSession>(HttpContext.Session.GetString("SessionUser"));

                HttpContext.Session.SetInt32("LoginID", customer.ID);
                HttpContext.Session.SetString("LoginName", customer.FirstName);
                HttpContext.Session.SetString("LoginMail", customer.Mail);
                HttpContext.Session.SetInt32("LoginLevel", customer.Level);
                HttpContext.Session.SetInt32("LoginStatus", customer.Status);

                if (customer.Level == 1)
                {
                    ViewBag.log = 1;
                    return View();
                }
                else
                {
                    //HttpContext.Session.SetString("Name", customer.LastName);
                    ViewBag.log = 2;
                    return RedirectToAction("Users", "Admin");
                }
            }
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
            if (_serviceCustomer.GetUser(model.Mail) == true)
            {
                ViewBag.check = 0;
                return View();
            }
            else
            {
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

        public IActionResult Logout()
        { 
            HttpContext.Session.Remove("LoginID");
            HttpContext.Session.Remove("LoginName");
            HttpContext.Session.Remove("LoginMail");
            HttpContext.Session.Remove("LoginLevel");
            HttpContext.Session.Remove("LoginStatus");

            return RedirectToAction("Index", "Home");
        }

        public IActionResult Contact()
        {
            return View();
        }
    }

}