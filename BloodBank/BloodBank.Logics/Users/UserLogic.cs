using BloodBank.Logics.Interfaces;
using BloodBank.Logics.Repositories;
using BloodBank.Logics.Users.DataHolders;
using BloodBank.Models;
using FluentValidation;
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

        private readonly Lazy<IValidator<User>> _userValidator;
        protected IValidator<User> UserValidator => _userValidator.Value;

        private readonly Lazy<IValidator<BloodDonator>> _bloodDonatorValidator;
        protected IValidator<BloodDonator> BloodDonatorValidator => _bloodDonatorValidator.Value;

        private readonly Lazy<IAuthService> _authService;
        protected IAuthService AuthService => _authService.Value;

        public UserLogic(Lazy<IUserRepository> userRepository,
            Lazy<IBloodTypeRepository>bloodTypeRepository,
            Lazy<IValidator<User>>userValidator,
            Lazy<IValidator<BloodDonator>>bloodDonatorValidator,
            Lazy<IAuthService> authService)
        {
            _userRepository = userRepository;
            _bloodTypeRepository = bloodTypeRepository;
            _userValidator = userValidator;
            _bloodDonatorValidator = bloodDonatorValidator;
            _authService = authService;
        }

        public Result<User> AddWorker(User model)
        {
            if (model == null)
            {
                return Result.Error<User>("User was null");
            }

            var userInDB = UserRepository.GetUserByLogin(model.Email);
            //check if user already exists in database
            //and if he is worker
            //if so return error
            if (userInDB != null && userInDB.Role.IsWorker)
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
                var validationResult = UserValidator.Validate(model, ruleSet: "ValidatePassword,ValidateEmail") ;

                if (!validationResult.IsValid)
                {
                    return Result.Error<User>(validationResult.Errors);
                }

                var hashPassword = AuthService.HashPassword(model.Password);

                if (hashPassword == null)
                {
                    return Result.Error<User>("Error while hashing paasword");
                }
                
                model.Password = hashPassword;

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
                return Result.Error<User>("User was null");
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
                var validationResult = BloodDonatorValidator.Validate(model.BloodDonator,ruleSet: "ValidateRestOfProperties,PhoneAndAdress");

                if (!validationResult.IsValid)
                {
                    return Result.Error<User>(validationResult.Errors);
                }

                userInDb.BloodDonator = model.BloodDonator;

                userInDb.Role.IsBloodDonator = true;
            }
            //if user doesent already exist, add new user that is blood donator but isnt worker

            else if (userInDb == null)
            {
                var userValidationResult = UserValidator.Validate(model,ruleSet:"*");

                if (!userValidationResult.IsValid)
                {
                    return Result.Error<User>(userValidationResult.Errors);
                }

                var bloodDonatorValidationResult = BloodDonatorValidator.Validate(model.BloodDonator);

                if (!bloodDonatorValidationResult.IsValid)
                {
                    return Result.Error<User>(bloodDonatorValidationResult.Errors);
                }

                var hashPassword = AuthService.HashPassword(model.Password);

                if (hashPassword == null)
                {
                    return Result.Error<User>("Error while adding new user");
                }

                model.Password = hashPassword;

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
            if (model == null) 
            {
                return Result.Error <User> ("Data was empty");
            }

            UserRepository.Delete(model);

            UserRepository.SaveChanges();

            return Result.Ok(model);
        }

        public Result<User> GetByEmail(string email)
        {
            if (string.IsNullOrEmpty(email))
            {
                return Result.Error<User>("Email was null or empty");
            }

            var result = UserRepository.GetUserByLogin(email);

            if (result == null) 
            {
                return Result.Error<User>("There is no user with that email");
            }

            return Result.Ok(result);
        }

        public Result<UserToken> Login(UserLoginAndPassword data)
        {
            if(data == null)
            {
                return Result.Error<UserToken>("Data was null");
            }

            var validationResult = AuthService.VerifyPassword(data.Login, data.Password);

            if (!validationResult)
            {
                return Result.Error<UserToken>("Error durning verification");
            }

            var user = UserRepository.GetUserByLogin(data.Login);

            var tokenData = AuthService.GenerateToken(user.Email, user.Role.IsWorker, user.Role.IsBloodDonator);

            if (tokenData == null) 
            {
                return Result.Error<UserToken>("Error durning token generation");
            }

            return Result.Ok(tokenData);
        }

        public Result<User> UpdatePassword(string email, string newPassword)
        {
            if(string.IsNullOrEmpty(email) || string.IsNullOrEmpty(newPassword))
            {
                return Result.Error<User>("Email or new password, something was empty or null");
            }

            var loggedInUser = UserRepository.GetUserByLogin(email);

            if (loggedInUser == null) 
            {
                return Result.Error<User>("Can't get a user with that email");
            }

            loggedInUser.Password = newPassword;

            var validationResult = UserValidator.Validate(loggedInUser, ruleSet: "ValidatePassword");

            if (!validationResult.IsValid)
            {
                return Result.Error<User>(validationResult.Errors);
            }

            var hashPassword = AuthService.HashPassword(loggedInUser.Password);

            if (hashPassword == null) 
            {
                return Result.Error<User>("Error durning hashing of password");
            }

            loggedInUser.Password = hashPassword;

            UserRepository.SaveChanges();

            return Result.Ok(loggedInUser);
        }

        public Result<User> UpdateDonator(string email, string newPhoneNumber, string newHomeAdress)
        {
            if (string.IsNullOrEmpty(email))
            {
                return Result.Error<User>("Password was null or empty");
            }

            if(string.IsNullOrEmpty(newPhoneNumber) && string.IsNullOrEmpty(newHomeAdress))
            {
                return Result.Error<User>("At least one variable cannot be null to be able to update");
            }

            var loggedInUser = UserRepository.GetUserByLogin(email);

            if (loggedInUser == null)
            {
                return Result.Error<User>("Can't get a user with that email");
            }

            if (!string.IsNullOrEmpty(newPhoneNumber))
            {
                loggedInUser.BloodDonator.PhoneNumber = newPhoneNumber;
            }

            if (!string.IsNullOrEmpty(newHomeAdress))
            {
                loggedInUser.BloodDonator.HomeAdress = newHomeAdress;
            }

            var validationResult = BloodDonatorValidator.Validate(loggedInUser.BloodDonator, ruleSet: "PhoneAndAdress");

            if (!validationResult.IsValid)
            {
                return Result.Error<User>(validationResult.Errors);
            }

            UserRepository.SaveChanges();

            return Result.Ok(loggedInUser);
        }

        public Result<string> Delete(string email)
        {
            if (string.IsNullOrEmpty(email))
            {
                return Result.Error<string>("Email was empty");
            }

            var userToDelete = UserRepository.GetUserByLogin(email);

            if (userToDelete == null) 
            {
                return Result.Error<string>("Couldnt find a user with that email");
            }

            UserRepository.Delete(userToDelete);
            UserRepository.SaveChanges();

            return Result.Ok(email);
        }
    }
}
