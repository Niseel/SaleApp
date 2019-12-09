using ApplicationCore.DTOs;
using ApplicationCore.Interfaces;
using Microsoft.AspNetCore.Mvc.Rendering;
using SaleApp.Interfaces;
using SaleApp.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SaleApp.Services
{
    public class UserIndexVmService : IUserIndexVmService
    {
        private readonly IUserService _service;
        public UserIndexVmService(IUserService userService)
        {
            _service = userService;
        }

        public UserCreateVm GetList()
        {
            return new UserCreateVm
            {
                StatusList = new SelectList(new List<SelectListItem>
                {
                    new SelectListItem { Text = "Unavailable", Value = "0"},
                    new SelectListItem { Text = "Availability", Value = "1"}
                }, "Value", "Text", 1)
            };
        }

        public IndexVm GetUserListVm(string searchString, int pageIndex = 1, int pageSize = 5, int? status = null)
        {
            var users = _service.GetAllUser(searchString, status);
            return new IndexVm
            {
                Total = users.Count(),
                Users = PaginatedList<UserDto>.Create(users, pageIndex, pageSize)
            };
        }
    }
}
