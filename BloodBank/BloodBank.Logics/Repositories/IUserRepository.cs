using BloodBank.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BloodBank.Logics.Repositories
{
    public interface IUserRepository:IRepository<User>
    {
        string GetUserPassword(string email);
        User GetUserByLogin(string email)
    }
}
