using BloodBank.Logics.Interfaces;
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

        public UserLogic(Lazy<IUserRepository> userRepository)
        {
            _userRepository = userRepository;
        }

        public Result<User> AddWorker(User model)
        {
            if(model == null)
            {
                throw new ArgumentNullException(nameof(model));
            }
            model.Role = new Role()
            {
                IsWorker = true,
                IsBloodDonator = false
            };
            UserRepository.Add(model);
            UserRepository.SaveChanges();

            return Result.Ok(model);
        }

        public Result<User> AddBloodDonator(User model)
        {
            if (model == null)
            {
                throw new ArgumentNullException(nameof(model));
            }
            model.Role = new Role()
            {
                IsWorker = false,
                IsBloodDonator = true
            };
            UserRepository.Add(model);
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
