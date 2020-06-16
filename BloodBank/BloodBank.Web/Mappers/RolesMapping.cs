using AutoMapper;
using BloodBank.Models;
using BloodBank.Web.DTO.Roles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BloodBank.Web.Mappers
{
    public class RolesMapping:Profile
    {
        public RolesMapping()
        {
            CreateMap<Role, ReturnRolesDTO>();
        }
    }
}
