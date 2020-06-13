using BloodBank.Logics.Repositories;
using BloodBank.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BloodBank.DataAccess
{
    public class BloodTypeRepository:Repository<BloodType>,IBloodTypeRepository
    {
        public BloodTypeRepository(Lazy<DataContext> dataContext)
            : base(dataContext)
        {

        }

        public BloodType GetByGroupName(string name)
        {
            return DataContext.BloodTypes.FirstOrDefault(m => m.BloodGroupName == name);
        }
    }
}
