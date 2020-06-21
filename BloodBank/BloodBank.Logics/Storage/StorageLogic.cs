using BloodBank.Logics.Interfaces;
using BloodBank.Logics.Repositories;
using BloodBank.Models;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace BloodBank.Logics.Storage
{
    public class StorageLogic:IStorageLogic
    {
        private readonly Lazy<IStorageRepository> _storageRepository;
        protected IStorageRepository StorageRepository => _storageRepository.Value;

        private readonly Lazy<IBloodTypeRepository> _bloodTypeRepository;
        protected IBloodTypeRepository BloodTypeRepository => _bloodTypeRepository.Value;

        private readonly Lazy<IValidator<BloodType>> _bloodTypeValidator;
        protected IValidator<BloodType> BloodTypeValidator => _bloodTypeValidator.Value;

        public StorageLogic(Lazy<IStorageRepository> storageRepository,
            Lazy<IBloodTypeRepository>bloodTypeRepository,
            Lazy<IValidator<BloodType>> bloodTypeValidator)
        {
            _storageRepository = storageRepository;
            _bloodTypeRepository = bloodTypeRepository;
            _bloodTypeValidator = bloodTypeValidator;
        }

        public Result<BloodStorage> Add(BloodStorage model)
        {
            if(model == null)
            {
                throw new ArgumentNullException(nameof(model));
            }

            if (BloodTypeRepository.CheckIfExists(model.BloodType.BloodGroupName))
            {
                return Result.Error<BloodStorage>("Blood type already in database");
            }

            var validationResult = BloodTypeValidator.Validate(model.BloodType);
            if (!validationResult.IsValid)
            {
                return Result.Error<BloodStorage>(validationResult.Errors);
            }

            StorageRepository.Add(model);
            StorageRepository.SaveChanges();

            return Result.Ok(model);
        }

        public Result<BloodStorage> Remove(BloodStorage model)
        {
            if(model == null)
            {
                throw new ArgumentNullException(nameof(model));
            }
            StorageRepository.Delete(model);
            StorageRepository.SaveChanges();
            return Result.Ok(model);
        }

        public Result<BloodStorage> GetByBloodTypeName(string name)
        {
            var result = StorageRepository.GetByBloodTypeName(name);
            if(result == null)
            {
                return Result.Error<BloodStorage>("There is no blood stored with that blood type");
            }
            return Result.Ok(result);
        }

        public Result<BloodStorage>AddBloodToStorage(string name,int ammount)
        {
            var bloodStorage = StorageRepository.GetByBloodTypeName(name);
            if(bloodStorage== null)
            {
                return Result.Error<BloodStorage>("There is no blood stored with that blood type");
            }
            bloodStorage.StoredAmmount += ammount;
            StorageRepository.SaveChanges();
            return Result.Ok(bloodStorage);
        }

        public Result<IEnumerable<BloodStorage>> GetAll()
        {
            return Result.Ok(StorageRepository.GetAll());
        }
    }
}
