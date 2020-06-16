using AutoMapper;
using BloodBank.Models;
using BloodBank.Web.DTO.BloodStorage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BloodBank.Web.Mappers
{
    public class BloodStorageProfile:Profile
    {
        public BloodStorageProfile()
        {
            CreateMap<AddBloodStorageDTO, BloodStorage>();
            CreateMap<BloodStorage, ReturnBloodStorageDTO>();
        }
    }
}
