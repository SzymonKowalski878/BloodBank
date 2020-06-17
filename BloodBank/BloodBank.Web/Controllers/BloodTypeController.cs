using AutoMapper;
using BloodBank.Logics.Interfaces;
using BloodBank.Models;
using BloodBank.Web.DTO.BloodStorage;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BloodBank.Web.Controllers
{
    [ApiController]
    [Route("api/controller")]
    public class BloodTypeController:Controller
    {
        private readonly Lazy<IBloodTypeLogic> _bloodTypeLogic;
        protected IBloodTypeLogic BloodTypeLogic => _bloodTypeLogic.Value;
        private readonly Lazy<IStorageLogic> _storageLogic;
        protected IStorageLogic StorageLogic => _storageLogic.Value;
        private readonly Lazy<IMapper> _mapper;
        protected IMapper Mapper => _mapper.Value;
        public BloodTypeController(Lazy<IBloodTypeLogic> bloodTypeLogic,
            Lazy<IStorageLogic> storageLogic,
            Lazy<IMapper> mapper)
        {
            _bloodTypeLogic = bloodTypeLogic;
            _mapper = mapper;
            _storageLogic = storageLogic;
        }

        [HttpPost,Route("AddBloodType")]
        public IActionResult Post(AddBloodStorageDTO dto)
        {
            if(dto == null)
            {
                throw new ArgumentNullException(nameof(dto));
            }
            var storageToAdd = Mapper.Map<AddBloodStorageDTO, BloodStorage>(dto);
            var result = StorageLogic.Add(storageToAdd);
            if (!result.IsScuccessful)
            {
                return BadRequest(result);
            }
            return Ok(dto);
        }

        [HttpDelete,Route("DeleteBloodType")]
        public IActionResult Delete(string name)
        {
            var toDelete = BloodTypeLogic.GetByName(name);
            if (!toDelete.IsScuccessful)
            {
                return BadRequest(toDelete);
            }

            var result = BloodTypeLogic.Remove(toDelete.Value);
            if (!result.IsScuccessful)
            {
                return BadRequest(result);
            }
            return Ok(result.Value);
        }

    }
}
