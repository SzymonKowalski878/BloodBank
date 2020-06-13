using BloodBank.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace BloodBank.Logics.Interfaces
{
    public interface IStorageLogic:ILogic
    {
        Result<BloodStorage> Add(BloodStorage model);
        Result<BloodStorage> Remove(BloodStorage model);
        Result<BloodStorage> GetByBloodTypeName(string name);
        Result<BloodStorage> AddBloodToStorage(string name, int ammount);
    }
}
