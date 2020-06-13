using BloodBank.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BloodBank.DataAccess
{
    public class BloodStorageRepository:Repository<BloodStorage>
    {
        public BloodStorageRepository(Lazy<DataContext> dataContext)
            : base(dataContext)
        {

        }

        public BloodStorage GetByBloodTypeName(string name)
        {
            return DataContext.Storage.FirstOrDefault(m => m.BloodGroupName == name);
        }
    }
}
