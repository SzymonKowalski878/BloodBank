using BloodBank.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace BloodBank.Logics.Interfaces
{
    public interface IBloodDonatorLogic:ILogic
    {
        Result<BloodDonator> UpdateDonatedBloodByUser(string pesel, int ammount);
    }
}
