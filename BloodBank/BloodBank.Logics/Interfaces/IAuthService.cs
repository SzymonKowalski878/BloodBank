using BloodBank.Logics.Users.DataHolders;
using System;
using System.Collections.Generic;
using System.Text;

namespace BloodBank.Logics.Interfaces
{
    public interface IAuthService:ILogic
    {
        UserToken GenerateToken(string login, bool isWorker, bool isDonator);
        string HashPassword(string password);
        bool VerifyPassword(string login, string password);
    }
}
