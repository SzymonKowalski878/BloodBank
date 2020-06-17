using BloodBank.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace BloodBank.Logics.Repositories
{
    public interface IBloodTypeRepository:IRepository<BloodType>
    {
        BloodType GetByGroupName(string name);
        bool CheckIfExists(string name);
    }
}
