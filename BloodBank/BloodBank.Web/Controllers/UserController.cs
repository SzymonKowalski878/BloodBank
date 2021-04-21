using AutoMapper;
using BloodBank.Logics;
using BloodBank.Logics.Interfaces;
using BloodBank.Logics.Users.DataHolders;
using BloodBank.Logics.Validators;
using BloodBank.Models;
using BloodBank.Web.DTO.BloodDonator;
using BloodBank.Web.DTO.Users;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Security.Claims;
using System.Threading.Tasks;

namespace BloodBank.Web.Controllers
{
    [ApiController]
    [Route("api/controller")]
    public class UserController:Controller
    {
        private readonly Lazy<IUserLogic> _userLogic;
        protected IUserLogic UserLogic => _userLogic.Value;

        private readonly Lazy<IMapper> _mapper;
        protected IMapper Mapper => _mapper.Value;

        private readonly Lazy<IBloodDonatorLogic> _bloodDonatorLogic;
        protected IBloodDonatorLogic BloodDonatorLogic => _bloodDonatorLogic.Value;

        public UserController(Lazy<IUserLogic> userLogic,
            Lazy<IMapper> mapper,
            Lazy<IBloodDonatorLogic> bloodDonatorLogic)
        {
            _userLogic = userLogic;
            _mapper = mapper;
            _bloodDonatorLogic = bloodDonatorLogic;
        }

        [Authorize(Policy = "Worker")]
        [HttpPost,Route("AddWorker")]

        public IActionResult Post(AddUserDTO dto)
        {
            var userToAdd = Mapper.Map<AddUserDTO, User>(dto);

            var result = UserLogic.AddWorker(userToAdd);

            if (!result.IsScuccessful)
            {
                return BadRequest(result);
            }

            return Ok(dto);
        }

        [Authorize(Policy ="Worker")]
        [HttpPost,Route("AddBloodDonator")]
        public IActionResult Post(AddDonatorDTO dto)
        {
            var donatorToAdd = Mapper.Map<AddDonatorDTO, User>(dto);

            var result = UserLogic.AddBloodDonator(donatorToAdd);

            if (!result.IsScuccessful)
            {
                return BadRequest(result.ErrorMessages);
            }

            return Ok(dto);
        }

        [Authorize(Policy = "Worker")]
        [HttpGet,Route("GetUserByLogin")]
        public IActionResult Get(string email)
        {
            var result = UserLogic.GetByEmail(email);

            if (!result.IsScuccessful)
            {
                return BadRequest(result);
            }

            return Ok(Mapper.Map<User, ReturnUserDTO>(result.Value));
        }

        [HttpPost, Route("Login")]
        public IActionResult Post(UserLoginAndPassword data)
        {
            var result = UserLogic.Login(data);

            if (!result.IsScuccessful)
            {
                return BadRequest(result);
            }

            return Ok(result.Value);
        }

        [Authorize("Donator")]
        [HttpGet,Route("GetLoggedInDonator")]
        public IActionResult Get()
        {
            var identity = HttpContext.User.Identity as ClaimsIdentity;

            var loggedInUserLogin = identity.FindFirst(ClaimTypes.Name)?.Value;

            var result = UserLogic.GetByEmail(loggedInUserLogin);

            if (!result.IsScuccessful)
            {
                return BadRequest(result);
            }

            var mappedUser = Mapper.Map<User, ReturnDonatorDTO>(result.Value);

            return Ok(mappedUser);
        }

        [Authorize]
        [HttpPatch, Route("UpdatePassword")]
        public IActionResult Patch(UpdatePasswordDTO dto)
        {
            var identity = HttpContext.User.Identity as ClaimsIdentity;

            var loggedInUserLogin = identity.FindFirst(ClaimTypes.Name)?.Value;

            var updateResult = UserLogic.UpdatePassword(loggedInUserLogin, dto.newPassword);

            if (!updateResult.IsScuccessful)
            {
                return BadRequest(updateResult.ErrorMessages);
            }

            return Ok();
        }

        [Authorize("Donator")]
        [HttpPatch,Route("UpdateDonator")]
        public IActionResult Patch(UpdateBloodDonatorDTO dto)
        {
            var identity = HttpContext.User.Identity as ClaimsIdentity;

            var loggedInUserLogin = identity.FindFirst(ClaimTypes.Name)?.Value;

            var updateResult = UserLogic.UpdateDonator(loggedInUserLogin, dto.PhoneNumber, dto.HomeAdress);

            if (!updateResult.IsScuccessful)
            {
                return BadRequest(updateResult.ErrorMessages);
            }

            return Ok(dto);
        }

        [HttpPatch,Route("AddDonatedBloodToUser")]
        public IActionResult Patch(UpdateAmmountOfDonatedBloodDTO dto)
        {
            var result = BloodDonatorLogic.UpdateDonatedBloodByUser(dto.Pesel, dto.AmmountOfBlood);

            if (!result.IsScuccessful)
            {
                return BadRequest(result);
            }

            return Ok(result.Value);
        }

        [Authorize("Worker")]
        [HttpDelete,Route("DelteUser")]
        public IActionResult Delete(EmailDTO email)
        {
            var result = UserLogic.Delete(email.Email);

            if (!result.IsScuccessful)
            {
                return BadRequest(result.ErrorMessages);
            }

            return Ok(result.Value);
        }
    }
}
