using BloodBank.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace BloodBank.Logics.Repositories
{
    public interface IBloodDonatorRepository:IRepository<BloodDonator>
    {
        BloodDonator GetByPesel(string pesel);
    }
}
