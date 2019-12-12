using ApplicationCore.DTOs;
using ApplicationCore.Entities;
using ApplicationCore.Interfaces;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;

namespace ApplicationCore.Services
{
    public class UserService : IUserService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public UserService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public IEnumerable<UserDto> GetAll()
        {
            var users = _unitOfWork.Users.GetAll();
            return _mapper.Map<IEnumerable<User>, IEnumerable<UserDto>>(users);
        }
        public IEnumerable<UserDto> GetAllUser(string searchString, int? status)
        {
            return _unitOfWork.Users.GetAllUser(searchString, status);
        }
            
        public UserDto GetUser(int id)
        {
            var user = _unitOfWork.Users.GetBy(id);
            return _mapper.Map<User, UserDto>(user);
        }
        public void Add(SaveUserDto saveUserDto)
        {
            var user = _mapper.Map<SaveUserDto, User>(saveUserDto);
            _unitOfWork.Users.Add(user);
            _unitOfWork.Complete();
        }

        //public void Delete(int? id)
        //{
        //    var user = _unitOfWork.Users.GetBy(id.Value);
        //    if (user != null)
        //    {
        //        _unitOfWork.Users.Remove(user);
        //        _unitOfWork.Complete();
        //    }
        //}

        public void Update(SaveUserDto saveUserDto)
        {
            var user = _unitOfWork.Users.GetBy(saveUserDto.ID);
            if (user == null) return;
            _mapper.Map<SaveUserDto, User>(saveUserDto, user);
            _unitOfWork.Complete();
        }

        public UserDto GetUser(string mail, string password)
        {
            return _unitOfWork.Users.GetUser(mail, password);
        }
        public bool GetUser(string mail)
        {
            var user = _unitOfWork.Users.GetUser(mail);
            return user != null ? true : false;
        }
    }
}
