using ApplicationCore.DTOs;
using ApplicationCore.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace ApplicationCore.Interfaces
{
    public interface IUserRepository : IRepository<User>
    {
        public IEnumerable<UserDto> GetAllUser(string searchString, int? status);

    }
}
