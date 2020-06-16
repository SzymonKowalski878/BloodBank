﻿using BloodBank.Logics.Interfaces;
using BloodBank.Logics.Repositories;
using BloodBank.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace BloodBank.Logics.Users
{
    public class UserLogic:IUserLogic
    {
        private readonly Lazy<IUserRepository> _userRepository;
        protected IUserRepository UserRepository => _userRepository.Value;
        private readonly Lazy<IBloodTypeRepository> _bloodTypeRepository;
        protected IBloodTypeRepository BloodTypeRepository => _bloodTypeRepository.Value;
        public UserLogic(Lazy<IUserRepository> userRepository,
            Lazy<IBloodTypeRepository>bloodTypeRepository)
        {
            _userRepository = userRepository;
            _bloodTypeRepository = bloodTypeRepository;
        }

        public Result<User> AddWorker(User model)
        {
            if(model == null)
            {
                throw new ArgumentNullException(nameof(model));
            }
            var userInDB = UserRepository.GetUserByLogin(model.Email);
            //check if user already exists in database
            //and if he is worker
            //if so return error
            if (userInDB!= null && userInDB.Role.IsWorker)
            {
                return Result.Error<User>("User already exists and is worker");
            }
            //check if user already exists in database
            //and if he isnt worker
            //if so make him worker
            else if (userInDB != null && !userInDB.Role.IsWorker)
            {
                userInDB.Role.IsWorker = true;
            }
            else
            //else just add user
            {
                model.Role = new Role()
                {
                    IsWorker = true,
                    IsBloodDonator = false
                };
                UserRepository.Add(model);
            }
            UserRepository.SaveChanges();

            return Result.Ok(model);
        }

        public Result<User> AddBloodDonator(User model)
        {
            if (model == null)
            {
                throw new ArgumentNullException(nameof(model));
            }
            //check if the bloodtype passed to the function exists in db
            if (!BloodTypeRepository.CheckIfExists(model.BloodDonator.BloodGroupName))
            {
                return Result.Error<User>("Blood type passed to the function doesen't exist in database");
            }

            var userInDb = UserRepository.GetUserByLogin(model.Email);
            //if user already exists and is blood donator return error
            if (userInDb != null && userInDb.Role.IsBloodDonator)
            {
                return Result.Error<User>("User already exists and is blood donator");
            }
            //if user already exists but isnt blood donator
            //make him a blooddonator and assign BloodDonator from model passed into the function
            //to BloodDonator in user that already exist
            else if (userInDb != null && !userInDb.Role.IsBloodDonator)
            {
                userInDb.BloodDonator = model.BloodDonator;
                userInDb.Role.IsBloodDonator = true;
            }
            //if user doesent already exist, add new user that is blood donator but isnt worker
            else if (userInDb == null)
            {
                model.Role = new Role()
                {
                    IsWorker = false,
                    IsBloodDonator = true
                };
                UserRepository.Add(model);
            }

            UserRepository.SaveChanges();
            return Result.Ok(model);
        }

        public Result<User> Remove(User model)
        {
            if(model == null)
            {
                throw new ArgumentNullException(nameof(model));
            }
            UserRepository.Delete(model);
            UserRepository.SaveChanges();
            return Result.Ok(model);
        }

        public Result<User> GetByEmail(string email)
        {
            var result = UserRepository.GetUserByLogin(email);
            if(result== null)
            {
                return Result.Error<User>("There is no user with that email");
            }
            return Result.Ok(result);
        }

    }
}
