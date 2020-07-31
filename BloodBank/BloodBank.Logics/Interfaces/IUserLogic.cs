using BloodBank.Logics.Users.DataHolders;
using BloodBank.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace BloodBank.Logics.Interfaces
{
    public interface IUserLogic:ILogic
    {
        Result<User> AddBloodDonator(User user);
        Result<User> AddWorker(User user);
        Result<User> Remove(User model);
        Result<User> GetByEmail(string email);
        Result<UserToken> Login(UserLoginAndPassword data);
    }
}
