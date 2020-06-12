using BloodBank.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace BloodBank.Logics.Repositories
{
    public interface IRepository<T> where T:BaseModel
    {
        void Delete(T model);
        void SaveChanges();
        IEnumerable<T> GetAll();
        void Add(T model);
    }
}
