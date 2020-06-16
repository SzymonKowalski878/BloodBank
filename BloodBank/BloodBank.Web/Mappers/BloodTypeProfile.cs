using AutoMapper;
using BloodBank.Models;
using BloodBank.Web.DTO.BloodTypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BloodBank.Web.Mappers
{
    public class BloodTypeProfile:Profile
    {
        public BloodTypeProfile()
        {
            CreateMap<BloodTypeDTO, BloodType>();
                
        }
    }
}
