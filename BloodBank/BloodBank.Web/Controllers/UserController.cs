using AutoMapper;
using BloodBank.Logics.Interfaces;
using BloodBank.Models;
using BloodBank.Web.DTO.Users;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
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
        public UserController(Lazy<IUserLogic> userLogic,
            Lazy<IMapper> mapper)
        {
            _userLogic = userLogic;
            _mapper = mapper;
        }

        [HttpPost,Route("AddWorker")]
        public IActionResult Post(AddUserDTO dto)
        {
            if(dto == null)
            {
                throw new ArgumentNullException(nameof(dto));
            }
            var userToAdd = Mapper.Map<AddUserDTO, User>(dto);
            var result = UserLogic.AddWorker(userToAdd);
            if (!result.IsScuccessful)
            {
                return BadRequest(result);
            }
            return Ok(dto);
        }

        [HttpPost,Route("AddBloodDonator")]
        public IActionResult Post(AddDonatorDTO dto)
        {
            if (dto == null)
            {
                throw new ArgumentNullException(nameof(dto));
            }
            var donatorToAdd = Mapper.Map<AddDonatorDTO, User>(dto);
            var result = UserLogic.AddBloodDonator(donatorToAdd);
            if (!result.IsScuccessful)
            {
                return BadRequest(result);
            }
            return Ok(dto);
        }
        
        [HttpGet,Route("GetUserByLogin")]
        public IActionResult Get(string email)
        {
            var result = UserLogic.GetByEmail(email);
            if (!result.IsScuccessful)
            {
                return BadRequest(result.Value);
            }
            return Ok(Mapper.Map<User, ReturnUserDTO>(result.Value));
        }
    }
}
