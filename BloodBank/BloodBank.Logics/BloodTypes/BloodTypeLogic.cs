using BloodBank.Logics.Interfaces;
using BloodBank.Logics.Repositories;
using BloodBank.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace BloodBank.Logics.BloodTypes
{
    public class BloodTypeLogic:IBloodTypeLogic
    {
        private readonly Lazy<IBloodTypeRepository> _bloodTypeRepository;
        protected IBloodTypeRepository BloodTypeRepository => _bloodTypeRepository.Value;

        public BloodTypeLogic(Lazy<IBloodTypeRepository> bloodTypeRepository)
        {
            _bloodTypeRepository = bloodTypeRepository;
        }

        public Result<BloodType> Add(BloodType model)
        {
            if(model== null)
            {
                throw new ArgumentNullException(nameof(model));
            }
            BloodTypeRepository.Add(model);
            BloodTypeRepository.SaveChanges();

            return Result.Ok(model);
        }

        public Result<BloodType> Remove(BloodType model)
        {
            if(model == null)
            {
                throw new ArgumentNullException(nameof(model));
            }
            BloodTypeRepository.Delete(model);
            BloodTypeRepository.SaveChanges();

            return Result.Ok(model);
        }

        public Result<BloodType> GetByName(string name)
        {
            var result = BloodTypeRepository.GetByGroupName(name);
            if (result == null)
            {
                return Result.Error<BloodType>("There is no blood type with that name");
            }
            return Result.Ok(result);
        }
    }
}
