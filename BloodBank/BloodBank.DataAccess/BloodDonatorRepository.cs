using BloodBank.Logics.Repositories;
using BloodBank.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BloodBank.DataAccess
{
    public class BloodDonatorRepository:Repository<BloodDonator>,IBloodDonatorRepository
    {
        public BloodDonatorRepository(Lazy<DataContext> dataContext)
            : base(dataContext)
        {

        }

        public BloodDonator GetByPesel(string pesel)
        {
            return DataContext.BloodDonators.FirstOrDefault(m => m.Pesel == pesel);
        }
    }
}
