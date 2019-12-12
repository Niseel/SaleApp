using ApplicationCore.DTOs;
using ApplicationCore.Entities;
using ApplicationCore.Interfaces;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using System.Security.Cryptography;
using SaleApp;

namespace Infrastructure.Persistence.Repositories
{
    public class UserRepository : Repository<User>, IUserRepository
    {
        public UserRepository(SaleContext context) : base(context)
        {
        }

        public IEnumerable<UserDto> GetAllUser(string searchString, int? status)
        {
            var query = from p in MContext.Users
                        select new
                        {
                            UserID = p.ID,
                            UserFirstName = p.FirstName,
                            UserLastName = p.LastName,
                            UserMail = p.Mail,
                            UserBirth = p.Birth,
                            UserPhone = p.Phone,
                            UserStatus = p.Status,
                            UserAddress = p.Address
                        };

            //query = query.Where(x => x.);
            if (status != null)
            {
                query = query.Where(s => s.UserStatus == status);
            }

            if (!String.IsNullOrEmpty(searchString))
            {
                query = query.Where(s => s.UserFirstName.Contains(searchString));
            }

            var x = query.ToList();
            List<UserDto> listResult = new List<UserDto>();
            foreach (var item in x)
            {
                UserDto users = new UserDto()
                {
                    ID = item.UserID,
                    FirstName = item.UserFirstName,
                    LastName = item.UserLastName,
                    Mail = item.UserMail,
                    Birth = item.UserBirth,
                    Phone = item.UserPhone,
                    Status = item.UserStatus,
                    Address = item.UserAddress
                };
                listResult.Add(users);
            }

            return listResult;
        }

        public UserDto GetUser(string mail, string pasword)
        {
            using (MD5 md5Hash = MD5.Create())
            {
                string hash = MD5Hash.GetMd5Hash(md5Hash, pasword);
                var query = from p in MContext.Users
                            where (string.Compare(p.Mail, mail) == 0) && (string.Compare(p.Password, hash) == 0)
                            select new
                            {
                                UserID = p.ID,
                                UserLastName = p.LastName,
                                UserFirstName = p.FirstName,
                                UserMail = p.Mail,
                                UserLevel = p.Level,
                                UserStatus = p.Status
                            };
                if (!query.Any())
                {
                    return null;
                }
                else
                {
                    var item = query.First();
                    UserDto user = new UserDto()
                    {
                        ID = item.UserID,
                        LastName = item.UserLastName,
                        FirstName = item.UserFirstName,
                        Mail = item.UserMail,
                        Level = item.UserLevel,
                        Status = item.UserStatus
                    };
                    return user;
                }
            }
        }

        public UserDto GetUser(string mail)
        {
            var query = from p in MContext.Users
                        where (string.Compare(p.Mail, mail) == 0)
                        select p;
            var item = query.First();
            if (item == null)
            {
                return null;
            }
            else
            {
                UserDto user = new UserDto();
                return user;
            }
        }
        protected SaleContext MContext
        {
            get { return Context as SaleContext; }
        }


    }
}
