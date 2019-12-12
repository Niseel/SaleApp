using SaleApp.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SaleApp.Interfaces
{
    public interface IUserIndexVmService
    {
        IndexVm GetUserListVm(string searchString, int pageIndex = 1, int pageSize = 5, int? status = null);
        UserCreateVm GetList();
        int GetAge(DateTime birthDate);
    }
}
