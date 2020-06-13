﻿using BloodBank.Logics.Repositories;
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

        public User GetUserByLogin(string email)
        {
            return DataContext.Users.FirstOrDefault(m => m.Email == email);
        }

        public string GetUserPassword(string email)
        {
            var result = GetUserByLogin(email);
            
            return result?.Password;
        }
    }
}
