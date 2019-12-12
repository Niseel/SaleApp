using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
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
    public class UsersController : Controller
    {
        private readonly IUserService _service;
        private readonly IMapper _mapper;
        [Obsolete]
        private readonly IHostingEnvironment _hostingEnvironment;

        private readonly IUserIndexVmService _userService;
        [Obsolete]
        public UsersController(IUserIndexVmService userService, IUserService service, IMapper mapper, IHostingEnvironment hostingEnvironment)
        {
            _userService = userService;
            _service = service;
            _mapper = mapper;
            _hostingEnvironment = hostingEnvironment;
        }
        public IActionResult Index(string searchString, int pageIndex = 1, int pageSize = 5, int? status = null)
        {
            if (HttpContext.Session.GetInt32("LoginLevel") != 2)
            {
                ViewBag.checkLogin = 0;
                return View("../Home/AddCart");
            }
            IndexVm UserIndexVM = _userService.GetUserListVm(searchString, pageIndex, pageSize, status);
            ViewBag.pageSize = pageSize;
            ViewBag.status = status;
            return View(UserIndexVM);
        }
        [Area("Admin")]
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
            var user = _service.GetUser(id.Value);


            if (user == null)
            {
                return NotFound();
            }

            return View(user);
        }

        [HttpGet]
        public IActionResult Create()
        {
            if (HttpContext.Session.GetInt32("LoginLevel") != 2)
            {
                ViewBag.checkLogin = 0;
                return View("../Home/AddCart");
            }
            var x = _userService.GetList();
            ViewBag.StatusList = x.StatusList;
            return View();
        }
        [HttpPost]
        [Obsolete]
        public IActionResult Create(UserCreateVm model)
        {
            if (HttpContext.Session.GetInt32("LoginLevel") != 2)
            {
                ViewBag.checkLogin = 0;
                return View("../Home/AddCart");
            }
            if (!ModelState.IsValid)
            {
                var x = _userService.GetList();
                ViewBag.StatusList = x.StatusList;
                return View();
            }

            string uniqueFileName = ProcessUploadedFile(model);
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
                Password = passHash,
                Birth = model.Birth,
                Address = model.Address,
                Phone = model.Phone,
                Status = model.Status,
                Level = model.Level,
                Note = model.Note,
                AvtPath = uniqueFileName
            };

            //Validate duplicates

            var users = _service.GetAll();
            int DuplicateCount = 0;
            foreach (UserDto item in users)
            {
                if (item.Phone == saveUserDto.Phone)
                {
                    ViewBag.PhoneDuplicateErrorMessage = "Error!";
                    DuplicateCount++;
                    break;
                }
            }
            foreach (UserDto item in users)
            {
                if (item.Mail.ToLower() == model.Mail.ToLower())
                {
                    ViewBag.MailDuplicateErrorMessage = "Error!";
                    DuplicateCount++;
                    break;
                }
            }

            //
            //
            //Validate Age >= 13
            var age = _userService.GetAge(saveUserDto.Birth);
            if (age < 13)
            {
                ViewBag.AgeErrorMessage = "Error!";
                DuplicateCount++;
            }


            if (DuplicateCount > 0) //Has Error
            {
                var x = _userService.GetList();
                ViewBag.StatusList = x.StatusList;
                return View();
            }

            _service.Add(saveUserDto);
            
            return RedirectToAction("Index");
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

            var user = _service.GetUser(id.Value);

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
        public IActionResult Edit(UserEditVm model)
        {
            if (HttpContext.Session.GetInt32("LoginLevel") != 2)
            {
                ViewBag.checkLogin = 0;
                return View("../Home/AddCart");
            }
            if (!ModelState.IsValid)
            {
                var x = _userService.GetList();
                ViewBag.StatusList = x.StatusList;
                return View(model);
            }

            var users = _service.GetAll();
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

            UserDto userDto = _service.GetUser(model.ID);
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
            _service.Update(saveUserDto);
            return RedirectToAction("Detail", _service.GetUser(saveUserDto.ID));
        }
        public IActionResult ResetPassword(int? id)
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

            UserDto userDto = _service.GetUser(id.Value);
            if (userDto == null)
            {
                return NotFound();
            }
            string passHash;
            using (MD5 md5Hash = MD5.Create())
            {
                passHash = MD5Hash.GetMd5Hash(md5Hash, "123456");
            }

            SaveUserDto saveUserDto = _mapper.Map<UserDto, SaveUserDto>(userDto);
            saveUserDto.Password = passHash;
            _service.Update(saveUserDto);
            return View("Thanks");
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