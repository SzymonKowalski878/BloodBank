using BloodBank.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace BloodBank.Logics.Repositories
{
    public interface IStorageRepository:IRepository<BloodStorage>
    {
        BloodStorage GetByBloodTypeName(string name);
    }
}
