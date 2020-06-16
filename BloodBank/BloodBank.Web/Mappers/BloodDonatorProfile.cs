using AutoMapper;
using BloodBank.Models;
using BloodBank.Web.DTO.BloodDonators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BloodBank.Web.Mappers
{
    public class BloodDonatorProfile:Profile
    {
        public BloodDonatorProfile()
        {
            CreateMap<AddBloodDonatorDTO, BloodDonator>();
        }
    }
}
