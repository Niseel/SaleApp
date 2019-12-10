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
            IndexVm UserIndexVM = _userService.GetUserListVm(searchString, pageIndex, pageSize, status);
            ViewBag.pageSize = pageSize;
            ViewBag.status = status;
            return View(UserIndexVM);
        }
        [Area("Admin")]
        public IActionResult Detail(int? id)
        {
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
            var x = _userService.GetList();
            ViewBag.StatusList = x.StatusList;
            return View();
        }
        [HttpPost]
        [Obsolete]
        public IActionResult Create(UserCreateVm model)
        {
            if (!ModelState.IsValid)
            {
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
            _service.Add(saveUserDto);
            
            return RedirectToAction("Index");
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