using AutoMapper;
using BloodBank.Logics.Interfaces;
using BloodBank.Models;
using BloodBank.Web.DTO.BloodStorage;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BloodBank.Web.Controllers
{
    [Route("api/controller")]
    [ApiController]
    public class StorageController:Controller
    {
        private readonly Lazy<IStorageLogic> _storageLogic;
        protected IStorageLogic StorageLogic => _storageLogic.Value;
        private readonly Lazy<IMapper> _mapper;
        protected IMapper Mapper => _mapper.Value;
        public StorageController(Lazy<IStorageLogic> storageLogic,
            Lazy<IMapper>mapper)
        {
            _storageLogic = storageLogic;
            _mapper = mapper;

        }

        [HttpGet,Route("GetStorageByGroupTypeName")]
        public IActionResult Get(string name)
        {
            var result = StorageLogic.GetByBloodTypeName(name);
            if (!result.IsScuccessful)
            {
                return BadRequest(result);
            }
            return Ok(Mapper.Map<BloodStorage, ReturnBloodStorageDTO>(result.Value));
        }

        [HttpGet,Route("GetAll")]
        public IActionResult Get()
        {
            var result = StorageLogic.GetAll();
            return Ok(Mapper.Map<IEnumerable<BloodStorage>, IEnumerable<ReturnBloodStorageDTO>>(result.Value));
        }

        [Authorize("Worker")]
        [HttpPut,Route("ChangeAmmountOfBloodInStorage")]
        public IActionResult Put(string name,int ammount)
        {
            var result = StorageLogic.AddBloodToStorage(name, ammount);
            if (!result.IsScuccessful)
            {
                return BadRequest(result);
            }
            return Ok(Mapper.Map<BloodStorage,ReturnBloodStorageDTO>(result.Value));
        }
    }
}
