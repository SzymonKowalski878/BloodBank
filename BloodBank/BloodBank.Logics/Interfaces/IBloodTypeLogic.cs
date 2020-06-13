using BloodBank.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace BloodBank.Logics.Interfaces
{
    public interface IBloodTypeLogic:ILogic
    {
        Result<BloodType> Add(BloodType model);
        Result<BloodType> Remove(BloodType model);
        Result<BloodType> GetByName(string name);
    }
}
