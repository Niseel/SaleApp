using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Threading.Tasks;
using ApplicationCore.DTOs;
using ApplicationCore.Entities;
using ApplicationCore.Interfaces;
using ApplicationCore.Session;
using AutoMapper;
using Microsoft.AspNetCore.Hosting;
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
        private readonly IUserIndexVmService _userService;
        private readonly IMapper _mapper;
        [Obsolete]
        private readonly IHostingEnvironment _hostingEnvironment;

        [Obsolete]
        public HomeController(IProductIndexVmService productService, IProductService serviceProduct, IUserService serviceCustomer, IUserIndexVmService userService, IMapper mapper, IHostingEnvironment hostingEnvironment)
        {
            _serviceProduct = serviceProduct;
            _productService = productService;
            _serviceCustomer = serviceCustomer;
            _userService = userService;
            _mapper = mapper;
            _hostingEnvironment = hostingEnvironment;
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
        public IActionResult 
            Login()
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

            HttpContext.Session.Remove("CartID");
            HttpContext.Session.Remove("CartName");
            HttpContext.Session.Remove("CartImage");
            HttpContext.Session.Remove("CartQty");
            HttpContext.Session.Remove("CartPrice");

            return RedirectToAction("Index", "Home");
        }

        public IActionResult Contact()
        {
            return View();
        }

        [HttpGet]
        [HttpGet]
        public IActionResult Profile(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            if (HttpContext.Session.GetString("LoginName") == null)
            {
                ViewBag.checkLogin = 0;
                return View("AddCart");
            }

            var user = _serviceCustomer.GetUser(id.Value);

            if (user == null)
            {
                return NotFound();
            }

            UserEditVm UserEdit = new UserEditVm()
            {
                ID = user.ID, // hidden
                ExistAvtPath = user.AvtPath, // hidden
                Password = user.Password,
                Address = user.Address,
                Birth = user.Birth,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Level = user.Level,
                Mail = user.Mail,
                Note = user.Note,
                Phone = user.Phone,
                Status = user.Status
            };
            var x = _userService.GetList();
            ViewBag.StatusList = x.StatusList;
            return View(UserEdit);
        }

        [HttpPost]
        [Obsolete]
        public IActionResult Profile(UserEditVm model)
        {
            if (HttpContext.Session.GetString("LoginName") == null)
            {
                ViewBag.checkLogin = 0;
                return View("AddCart");
            }
            if (!ModelState.IsValid)
            {
                var x = _userService.GetList();
                ViewBag.StatusList = x.StatusList;
                return View(model);
            }

            var users = _serviceCustomer.GetAll();
            int ErrorCount = 0;
            foreach (UserDto item in users)
            {
                if (item.Phone == model.Phone && item.ID != model.ID)
                {
                    ViewBag.UserPhoneEditErrorMessage = "Error";
                    ErrorCount++;
                }
            }
            var age = _userService.GetAge(model.Birth);
            if (age < 13)
            {
                ViewBag.AgeErrorMessage = "Error!";
                ErrorCount++;
            }
            if (ErrorCount > 0)
            {
                var x = _userService.GetList();
                ViewBag.StatusList = x.StatusList;
                return View(model);
            }

            UserDto userDto = _serviceCustomer.GetUser(model.ID);
            SaveUserDto saveUserDto = _mapper.Map<UserDto, SaveUserDto>(userDto);
            saveUserDto.LastName = model.LastName;
            saveUserDto.FirstName = model.FirstName;
            saveUserDto.Address = model.Address;
            saveUserDto.Birth = model.Birth;
            saveUserDto.Level = model.Level;
            saveUserDto.Mail = model.Mail;
            saveUserDto.Note = model.Note;
            //saveUserDto.Password = model.Password;
            saveUserDto.Phone = model.Phone;
            saveUserDto.Status = model.Status;

            if (model.Avt != null)
            {
                if (model.ExistAvtPath != null)
                {
                    string filePath = Path.Combine(_hostingEnvironment.WebRootPath, "images", model.ExistAvtPath);
                    System.IO.File.Delete(filePath);
                }
                saveUserDto.AvtPath = ProcessUploadedFile(model);
            }
            _serviceCustomer.Update(saveUserDto);
            var user = _serviceCustomer.GetUser(model.ID);
            var id = user.ID;
            return RedirectToAction("Profile", id);
        }

        public IActionResult Cart()
        {
            if (HttpContext.Session.GetInt32("CartID") != null)
            {
                return View();

            }
            else
            {
                ViewBag.checkCart = 0;
                return View();
            }
        }

        public IActionResult AddCart(int? id)
        {
            if(HttpContext.Session.GetString("LoginName") == null)
            {
                ViewBag.checkLogin = 0;
                return View();
            }
            else
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

                var productName = product.Name;
                var CartPrice = product.Price - (product.Price * product.Sale / 100);
                var productID = (product.ID).ToString();

                var newCart = new Cart();
                if (HttpContext.Session.GetString(productID) == null)
                {
                    newCart.CartID = product.ID;
                    newCart.CartName = product.Name;
                    newCart.CartImage = product.ImagePath;
                    newCart.CartQty = 1;
                    newCart.CartPrice = product.Price;
                }
                else
                {
                    newCart.CartQty += 1;
                }

                HttpContext.Session.SetString(product.Name, JsonConvert.SerializeObject(newCart));
                var productNamee = HttpContext.Session.GetString(product.Name);

                HttpContext.Session.SetString("SessionUser", productNamee);


                    //HttpContext.Session.SetInt32("CartID", product.ID);
                    //HttpContext.Session.SetString("CartName", product.Name);
                    //HttpContext.Session.SetString("CartImage", product.ImagePath);
                    //HttpContext.Session.SetInt32("CartQty", 1);
                    //HttpContext.Session.SetString("CartPrice", CartPrice.ToString());
                //}
                //else
                //{
                //    var CartQty = HttpContext.Session.GetInt32("CartQty");
                //    CartQty++;
                //    HttpContext.Session.SetInt32("CartQty", CartQty.Value);
                //}

                return RedirectToAction("Index");
            }
           
        }

        [Obsolete]
        private string ProcessUploadedFile(UserCreateVm model)
        {
            string uniqueFileName = null;
            if (model.Avt != null)
            {
                string uploadFolder = Path.Combine(_hostingEnvironment.WebRootPath, "images/users");
                uniqueFileName = Guid.NewGuid().ToString() + "_" + model.Avt.FileName;
                string filePath = Path.Combine(uploadFolder, uniqueFileName);
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    model.Avt.CopyTo(fileStream);
                }

            }

            return uniqueFileName;
        }
    }

}