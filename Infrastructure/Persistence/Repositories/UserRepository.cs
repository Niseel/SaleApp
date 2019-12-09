using ApplicationCore.DTOs;
using ApplicationCore.Entities;
using ApplicationCore.Interfaces;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;

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

        protected SaleContext MContext
        {
            get { return Context as SaleContext; }
        }


    }
}
