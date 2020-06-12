using BloodBank.Logics.Repositories;
using BloodBank.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BloodBank.DataAccess
{
    public class UserRepository:Repository<User>,IUserRepository
    {
        public UserRepository(Lazy<DataContext> dataContext)
            :base(dataContext)
        {
            
        }

        public IQueryable<User> GetUsersByLogin(string email)
        {
            return DataContext.Users.Where(m => m.Email == email);
        }

        public string GetUserPassword(string email)
        {
            var list = GetUsersByLogin(email);
            var result = list.FirstOrDefault();

            return result?.Password;
        }
    }
}
