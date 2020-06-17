using BloodBank.Logics.Repositories;
using BloodBank.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BloodBank.DataAccess
{
    public class StorageRepository:Repository<BloodStorage>,IStorageRepository
    {
        public StorageRepository(Lazy<DataContext> dataContext)
            : base(dataContext)
        {

        }

        public BloodStorage GetByBloodTypeName(string name)
        {
            return DataContext.Storage.FirstOrDefault(m => m.BloodGroupName == name);
        }
    }
}
