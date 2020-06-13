using BloodBank.Logics.Repositories;
using BloodBank.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace BloodBank.DataAccess
{
    public class Repository<T> : IRepository<T> where T : BaseModel
    {
        private readonly Lazy<DataContext> _dataContext;
        protected DataContext DataContext => _dataContext.Value;

        public Repository(Lazy<DataContext> dataContext)
        {
            _dataContext = dataContext;
        }

        public virtual void Delete(T model)
        {
            DataContext.Remove(model);
        }

        public virtual void SaveChanges()
        {
            DataContext.SaveChanges();
        }

        public virtual IEnumerable<T> GetAll()
        {
            return DataContext.Set<T>();
        }

        public virtual void Add(T model)
        {
            DataContext.Set<T>().Add(model);
        }
    }
}
