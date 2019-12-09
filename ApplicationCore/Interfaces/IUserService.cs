using ApplicationCore.DTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace ApplicationCore.Interfaces
{
    public interface IUserService
    {
        UserDto GetUser(int id);
        IEnumerable<UserDto> GetAll();
        IEnumerable<UserDto> GetAllUser(string searchString, int? status);
        void Add(SaveUserDto saveUserDto);
        //void Delete(int? id);
        void Update(SaveUserDto saveUserDto);
    }
}
